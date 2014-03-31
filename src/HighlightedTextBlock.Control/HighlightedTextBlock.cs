using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace HighlightedTextBlock
{
    [TemplatePart(Name = "PART_Content", Type = typeof(TextBlock))]
    [StyleTypedProperty(Property = "TextStyle", StyleTargetType = typeof(TextBlock))]
    public class HighlightedTextBlock : Control
    {
        public HighlightedTextBlock()
        {
            DefaultStyleKey = typeof(HighlightedTextBlock);
        }

        #region Dependency Properties

        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(HighlightedTextBlock), new PropertyMetadata("", OnPropertyChanged));

        /// <summary>
        /// Gets or sets the text to be displayed
        /// </summary>
        public string Text
        {
            get { return base.GetValue(TextProperty) as string; }
            set { base.SetValue(TextProperty, value); }
        }

        public static DependencyProperty TextStyleProperty = DependencyProperty.Register("TextStyle", typeof(Style), typeof(HighlightedTextBlock), new PropertyMetadata(null, OnPropertyChanged));

        /// <summary>
        /// Gets or sets the style of the text
        /// </summary>
        public Style TextStyle
        {
            get { return base.GetValue(TextStyleProperty) as Style; }
            set { base.SetValue(TextStyleProperty, value); }
        }

        public static DependencyProperty HighlightProperty = DependencyProperty.Register("Highlight", typeof(string), typeof(HighlightedTextBlock), new PropertyMetadata("", OnPropertyChanged));

        /// <summary>
        /// Gets or sets the text which is to be highlighted
        /// </summary>
        public string Highlight
        {
            get { return base.GetValue(HighlightProperty) as string; }
            set { base.SetValue(HighlightProperty, value); }
        }

        public static DependencyProperty HighlightColorProperty = DependencyProperty.Register("HighlightColor", typeof(Brush), typeof(HighlightedTextBlock), new PropertyMetadata(null, OnPropertyChanged));

        /// <summary>
        /// Gets or sets the color of the highlighted parts of the text
        /// </summary>
        public Brush HighlightColor
        {
            get { return base.GetValue(HighlightColorProperty) as Brush; }
            set { base.SetValue(HighlightColorProperty, value); }
        }

        #endregion

        #region Events

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as HighlightedTextBlock;

            if (d != null)
            {
                control.OnPropertyChanged(e.Property);
            }
        }

        private void OnPropertyChanged(DependencyProperty dependencyProperty)
        {
            OnApplyTemplate();
        }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var textblock = GetTemplateChild("PART_Content") as TextBlock;

            if (textblock == null)
            {
                return;
            }

            textblock.Inlines.Clear();

            var text = Text.ToLower();
            var highlight = Highlight.ToLower();

            if (highlight.Length > 0 && text.Contains(highlight))
            {
                int pos = 0;
                int index;

                while ((index = text.IndexOf(highlight, pos)) >= 0)
                {
                    if (pos < index)
                    {
                        textblock.Inlines.Add(new Run { Text = Text.Substring(pos, index - pos) });
                    }

                    var run = new Run { Text = Text.Substring(index, highlight.Length) };

                    if (HighlightColor != null)
                    {
                        run.Foreground = HighlightColor;
                    }

                    textblock.Inlines.Add(run);

                    pos = index + highlight.Length;
                }

                if (pos < text.Length - 1)
                {
                    textblock.Inlines.Add(new Run { Text = Text.Substring(pos) });
                }
            }
            else
            {
                textblock.Text = Text;
            }
        }

    }
}
