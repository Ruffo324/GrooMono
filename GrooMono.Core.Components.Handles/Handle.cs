// ReSharper disable once CheckNamespace

using System;
using System.Collections.Generic;
using System.Linq;
using GrooMono.Core.Components.Handles.Exceptions;
using GrooMono.Core.GrooGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// ReSharper disable once CheckNamespace
namespace GrooMono.Core.Components
{
    public class Handle : Sprite2D
    {
        private readonly Dictionary<string, Func<Handle, bool>> _contentRules =
            new Dictionary<string, Func<Handle, bool>>();

        public Handle(string contentName, float scale) : base(contentName, scale)
        {
            GameInstance.Instance.ManagerDraw.OnDraw += ManagerDrawOnOnDraw;
        }

        public bool Drawable { get; set; } = true;

        /// <summary>
        ///     Add a content, to this sprite, wich used, if the rule is true.
        /// </summary>
        /// <param name="contentName">Content, wich should be used.</param>
        /// <param name="expression">Expression, wich have to be true.</param>
        /// //TODO: Think about.. Create "Rules" logic only? Also nicht nur für Content, sondern das die Func selbst schon alles regelt..
        public void AddContentRule(string contentName, Func<Handle, bool> expression)
        {
            // There is already a rule for this content -> exception.
            if (_contentRules.ContainsKey(contentName))
                throw new ContentRuleException($"The entity, based on content \"{ContentName}\", " +
                                               $"had already a content rule for the content \"{contentName}\"." +
                                               "Use the function \"Entity.RemoveContentRule(contentName)\" first, to remove content rules.");
            // There is already a rule with the given expression -> exception.
            if (_contentRules.ContainsValue(expression))
                throw new ContentRuleException($"The entity, based on content \"{ContentName}\", " +
                                               "had already a content rule with the given expression." +
                                               "You can't use diffrent conents, on the same expression at the same time." +
                                               "Use the function \"Entity.RemoveContentRule(contentName)\" first, to remove content rules.");

            // Add new content rule
            _contentRules.Add(contentName, expression);
        }

        /// <summary>
        ///     Remove an expression content rule.
        /// </summary>
        /// <param name="contentName">Content name of the expression content rule.</param>
        /// <returns>True if rule exists, false if not.</returns>
        public bool RemoveContentRule(string contentName)
        {
            return _contentRules.Remove(contentName);
        }

        private void ManagerDrawOnOnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Not drawable -> return;
            if (!Drawable)
                return;

            // No content rule active, and current content is not the original content -> change to original content.
            if (!_contentRules.Any(pair =>
            {
                if (!pair.Value(this))
                    return false;

                ChangeContent(pair.Key);
                return true;
            }) && Texture.Name != ContentName)
                ChangeContent(ContentName);

            // Draw.
            Draw(spriteBatch);
        }
    }
}