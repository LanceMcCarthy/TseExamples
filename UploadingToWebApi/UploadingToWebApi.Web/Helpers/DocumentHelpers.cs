using System.Windows;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Editing.Flow;
using UploadingToWebApi.Web.Models;
using FillRule = Telerik.Windows.Documents.Fixed.Model.Graphics.FillRule;
using HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment;
using PathFigure = Telerik.Windows.Documents.Fixed.Model.Graphics.PathFigure;
using PathGeometry = Telerik.Windows.Documents.Fixed.Model.Graphics.PathGeometry;
using VerticalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.VerticalAlignment;
using FontFamily = System.Windows.Media.FontFamily;
using FontStyles = System.Windows.FontStyles;
using FontWeights = System.Windows.FontWeights;
using Point = System.Windows.Point;

namespace UploadingToWebApi.Web.Helpers
{
    public static class DocumentHelpers
    {
        private static readonly double defaultLeftIndent = 50;
        private static readonly double defaultLineHeight = 16;

        public static void DrawDescription(FixedContentEditor editor, double maxWidth)
        {
            Block block = new Block();
            block.GraphicProperties.FillColor = RgbColors.Black;
            block.HorizontalAlignment = HorizontalAlignment.Left;
            block.TextProperties.FontSize = 14;
            block.TextProperties.TrySetFont(new FontFamily("Calibri"), FontStyles.Italic, FontWeights.Bold);
            block.InsertText("RadPdfProcessing");
            block.TextProperties.TrySetFont(new FontFamily("Calibri"));
            block.InsertText(" is a document processing library that enables your application to import and export files to and from PDF format. The document model is entirely independent from UI and allows you to generate sleek documents with differently formatted text, images, shapes and more.");

            editor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));
        }

        public static void DrawText(FixedContentEditor editor, double maxWidth, MyPdfContent content)
        {
            double currentTopOffset = 470;
            currentTopOffset += defaultLineHeight * 2;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            editor.TextProperties.FontSize = 11;

            Block block = new Block();
            block.TextProperties.FontSize = 11;
            block.TextProperties.TrySetFont(new FontFamily("Arial"));
            block.InsertText("A wizard's job is to vex ");
            using (block.GraphicProperties.Save())
            {
                block.GraphicProperties.FillColor = new RgbColor(255, 146, 208, 80);
                block.InsertText("chumps");
            }

            block.InsertText(" quickly in fog.");
            editor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));

            currentTopOffset += defaultLineHeight;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);

            block = new Block();
            block.TextProperties.FontSize = 11;
            block.TextProperties.TrySetFont(new FontFamily("Trebuchet MS"));
            block.InsertText("A wizard's job is to vex chumps ");
            using (block.TextProperties.Save())
            {
                block.TextProperties.UnderlinePattern = UnderlinePattern.Single;
                block.TextProperties.UnderlineColor = editor.GraphicProperties.FillColor;
                block.InsertText("quickly");
            }

            block.InsertText(" in fog.");
            editor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));

            currentTopOffset += defaultLineHeight;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            block = new Block();
            block.TextProperties.FontSize = 11;
            block.TextProperties.TrySetFont(new FontFamily("Algerian"));
            block.InsertText("A ");
            using (block.TextProperties.Save())
            {
                block.TextProperties.UnderlinePattern = UnderlinePattern.Single;
                block.TextProperties.UnderlineColor = editor.GraphicProperties.FillColor;
                block.InsertText("wizard's");
            }

            block.InsertText(" job is to vex chumps quickly in fog.");
            editor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));

            currentTopOffset += defaultLineHeight;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);

            editor.TextProperties.TrySetFont(new FontFamily("Lucida Calligraphy"));
            editor.DrawText("A wizard's job is to vex chumps quickly in fog.", new Size(maxWidth, double.PositiveInfinity));

            currentTopOffset += defaultLineHeight + 2;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            block = new Block();
            block.TextProperties.FontSize = 11;
            block.TextProperties.TrySetFont(new FontFamily("Consolas"));
            block.InsertText("A wizard's job is to vex chumps ");
            using (block.TextProperties.Save())
            {
                block.TextProperties.TrySetFont(new FontFamily("Consolas"), FontStyles.Normal, FontWeights.Bold);
                block.InsertText("quickly");
            }

            block.InsertText(" in fog.");
            editor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));

            currentTopOffset += defaultLineHeight;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);
            editor.DrawText("El pingüino Wenceslao hizo kilómetros bajo exhaustiva lluvia y frío; añoraba a su querido cachorro.", new Size(maxWidth, double.PositiveInfinity));

            currentTopOffset += defaultLineHeight;
            editor.Position.Translate(defaultLeftIndent, currentTopOffset);

            // TO VERIFY CONTENT WAS UPLOADED THIS IS THE PASSED TEXT
            editor.DrawText(content.Body, new Size(maxWidth, double.PositiveInfinity));
        }

        public static void DrawFunnelFigure(FixedContentEditor editor)
        {
            editor.GraphicProperties.IsStroked = false;
            editor.GraphicProperties.FillColor = new RgbColor(231, 238, 247);
            editor.DrawEllipse(new Point(250, 70), 136, 48);

            editor.GraphicProperties.IsStroked = true;
            editor.GraphicProperties.StrokeColor = RgbColors.White;
            editor.GraphicProperties.StrokeThickness = 1;
            editor.GraphicProperties.FillColor = new RgbColor(91, 155, 223);
            editor.DrawEllipse(new Point(289, 77), 48, 48);

            editor.Position.Translate(291, 204);
            CenterText(editor, "Fonts");

            editor.Position.Translate(0, 0);
            editor.DrawEllipse(new Point(238, 274), 48, 48);
            editor.Position.Translate(190, 226);
            CenterText(editor, "Images");

            editor.Position.Translate(0, 0);
            editor.DrawEllipse(new Point(307, 347), 48, 48);
            editor.Position.Translate(259, 299);
            CenterText(editor, "Shapes");

            editor.Position.Translate(0, 0);
            Telerik.Windows.Documents.Fixed.Model.Graphics.PathGeometry arrow = new Telerik.Windows.Documents.Fixed.Model.Graphics.PathGeometry();
            PathFigure figure = arrow.Figures.AddPathFigure();
            figure.StartPoint = new Point(287, 422);
            figure.IsClosed = true;
            figure.Segments.AddLineSegment(new Point(287, 438));
            figure.Segments.AddLineSegment(new Point(278, 438));
            figure.Segments.AddLineSegment(new Point(300, 454));
            figure.Segments.AddLineSegment(new Point(322, 438));
            figure.Segments.AddLineSegment(new Point(313, 438));
            figure.Segments.AddLineSegment(new Point(313, 422));

            editor.DrawPath(arrow);

            editor.GraphicProperties.FillColor = new RgbColor(80, 255, 255, 255);
            editor.GraphicProperties.IsStroked = true;
            editor.GraphicProperties.StrokeThickness = 1;
            editor.GraphicProperties.StrokeColor = new RgbColor(91, 155, 223);

            Telerik.Windows.Documents.Fixed.Model.Graphics.PathGeometry funnel = new PathGeometry();
            funnel.FillRule = FillRule.EvenOdd;
            figure = funnel.Figures.AddPathFigure();
            figure.IsClosed = true;
            figure.StartPoint = new Point(164, 245);
            figure.Segments.AddArcSegment(new Point(436, 245), 136, 48);
            figure.Segments.AddArcSegment(new Point(164, 245), 136, 48);

            figure = funnel.Figures.AddPathFigure();
            figure.IsClosed = true;
            figure.StartPoint = new Point(151, 245);
            figure.Segments.AddArcSegment(new Point(449, 245), 149, 61);
            figure.Segments.AddLineSegment(new Point(332, 415));
            figure.Segments.AddArcSegment(new Point(268, 415), 16, 4);

            editor.DrawPath(funnel);

            editor.Position.Translate(164, 455);
            Block block = new Block();
            block.TextProperties.Font = editor.TextProperties.Font;
            block.GraphicProperties.FillColor = RgbColors.Black;
            block.HorizontalAlignment = HorizontalAlignment.Center;
            block.VerticalAlignment = VerticalAlignment.Top;
            block.TextProperties.FontSize = 18;
            block.InsertText("PDF");
            editor.DrawBlock(block, new Size(272, double.PositiveInfinity));
        }

        public static void CenterText(FixedContentEditor editor, string text)
        {
            Block block = new Block();
            block.TextProperties.TrySetFont(new FontFamily("Calibri"));
            block.HorizontalAlignment = HorizontalAlignment.Center;
            block.VerticalAlignment = VerticalAlignment.Center;
            block.GraphicProperties.FillColor = RgbColors.White;
            block.InsertText(text);

            editor.DrawBlock(block, new Size(96, 96));
        }
    }
}