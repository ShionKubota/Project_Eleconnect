// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Preview
{
    partial class MapSavedDialog
    {
        Label Label_1;
        Label Label_2;
        Button Button_1;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            Label_1 = new Label();
            Label_1.Name = "Label_1";
            Label_2 = new Label();
            Label_2.Name = "Label_2";
            Button_1 = new Button();
            Button_1.Name = "Button_1";

            // MapSavedDialog
            this.AddChildLast(Label_1);
            this.AddChildLast(Label_2);
            this.AddChildLast(Button_1);
            this.ShowEffect = new SlideInEffect()
            {
                MoveDirection = FourWayDirection.Up,
            };
            this.HideEffect = new TiltDropEffect();
            this.Showing += new EventHandler(onShowing);
            this.Shown += new EventHandler(onShown);

            // Label_1
            Label_1.TextColor = new UIColor(98f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            Label_1.Font = new UIFont(FontAlias.System, 40, FontStyle.Bold | FontStyle.Italic);
            Label_1.LineBreak = LineBreak.Character;
            Label_1.HorizontalAlignment = HorizontalAlignment.Center;

            // Label_2
            Label_2.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            Label_2.Font = new UIFont(FontAlias.System, 17, FontStyle.Regular);
            Label_2.LineBreak = LineBreak.Character;

            // Button_1
            Button_1.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            Button_1.TextFont = new UIFont(FontAlias.System, 16, FontStyle.Regular);

            SetWidgetLayout(orientation);

            UpdateLanguage();
        }

        private LayoutOrientation _currentLayoutOrientation;
        public void SetWidgetLayout(LayoutOrientation orientation)
        {
            switch (orientation)
            {
                case LayoutOrientation.Vertical:
                    this.SetPosition(0, 0);
                    this.SetSize(544, 960);
                    this.Anchors = Anchors.None;

                    Label_1.SetPosition(205, 184);
                    Label_1.SetSize(214, 36);
                    Label_1.Anchors = Anchors.None;
                    Label_1.Visible = true;

                    Label_2.SetPosition(187, 211);
                    Label_2.SetSize(214, 36);
                    Label_2.Anchors = Anchors.None;
                    Label_2.Visible = true;

                    Button_1.SetPosition(375, 416);
                    Button_1.SetSize(214, 56);
                    Button_1.Anchors = Anchors.None;
                    Button_1.Visible = true;

                    break;

                default:
                    this.SetPosition(0, 0);
                    this.SetSize(480, 272);
                    this.Anchors = Anchors.None;

                    Label_1.SetPosition(-9, 17);
                    Label_1.SetSize(499, 89);
                    Label_1.Anchors = Anchors.Top | Anchors.Bottom | Anchors.Left | Anchors.Right;
                    Label_1.Visible = true;

                    Label_2.SetPosition(29, 98);
                    Label_2.SetSize(421, 70);
                    Label_2.Anchors = Anchors.Top | Anchors.Bottom | Anchors.Left | Anchors.Right;
                    Label_2.Visible = true;

                    Button_1.SetPosition(181, 192);
                    Button_1.SetSize(117, 54);
                    Button_1.Anchors = Anchors.Top | Anchors.Bottom | Anchors.Height | Anchors.Left | Anchors.Right | Anchors.Width;
                    Button_1.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            Label_1.Text = "SUCCESSFULL";

            Label_2.Text = "※Eleconnect\\bin\\Debug\\Eleconnect-unsigned\\Documentsに保存しました。この情報をステージ情報クラスにコピーして使ってください。";

            Button_1.Text = "OK";
        }

        private void onShowing(object sender, EventArgs e)
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    Label_2.Visible = false;
                    break;

                default:
                    Label_2.Visible = false;
                    break;
            }
        }

        private void onShown(object sender, EventArgs e)
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    new FadeInEffect()
                    {
                        Widget = Label_2,
                    }.Start();
                    break;

                default:
                    new FadeInEffect()
                    {
                        Widget = Label_2,
                    }.Start();
                    break;
            }
        }

    }
}
