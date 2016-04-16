using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace space_prototype.UI
{
    public class Button
    {
        private readonly Texture2D _button_notselected;
        private readonly Texture2D _button_selected;
        public string ButtonText;

        public Vector2 Position;
        public Texture2D TButton;

        public Button(Texture2D selected, Texture2D deselected)
        {
            _button_notselected = deselected;
            _button_selected = selected;
            TButton = _button_selected;
        }

        public void Select()
        {
            TButton = _button_selected;
        }

        public void DeSelect()
        {
            TButton = _button_notselected;
        }

        public bool CursorOnButton(Vector2 cursor)
        {
            var rect = new Rectangle((int) Position.X, (int) Position.Y, TButton.Bounds.Width, TButton.Bounds.Height);
            Console.WriteLine(rect.ToString());
            Console.WriteLine(cursor.ToString());
            if (rect.Contains(cursor))
            {
                return true;
            }
            return false;
        }
    }
}