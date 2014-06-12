// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Preview
{
    partial class Clear
    {
        LiveFlipPanel LiveFlipPanel_1;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            LiveFlipPanel_1 = new LiveFlipPanel();
            LiveFlipPanel_1.Name = "LiveFlipPanel_1";

            // Clear
            this.RootWidget.AddChildLast(LiveFlipPanel_1);
            this.Showing += new EventHandler(onShowing);
            this.Shown += new EventHandler(onShown);

            // LiveFlipPanel_1
            LiveFlipPanel_1.FrontPanel = new panel1();
            LiveFlipPanel_1.BackPanel = new Panel2();
            LiveFlipPanel_1.FlipCount = 100;
            LiveFlipPanel_1.TouchEnabled = true;

            SetWidgetLayout(orientation);

            UpdateLanguage();
        }

        private LayoutOrientation _currentLayoutOrientation;
        public void SetWidgetLayout(LayoutOrientation orientation)
        {
            switch (orientation)
            {
                case LayoutOrientation.Vertical:
                    this.DesignWidth = 544;
                    this.DesignHeight = 960;

                    LiveFlipPanel_1.SetPosition(399, 190);
                    LiveFlipPanel_1.SetSize(100, 50);
                    LiveFlipPanel_1.Anchors = Anchors.None;
                    LiveFlipPanel_1.Visible = true;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    LiveFlipPanel_1.SetPosition(0, 0);
                    LiveFlipPanel_1.SetSize(960, 544);
                    LiveFlipPanel_1.Anchors = Anchors.None;
                    LiveFlipPanel_1.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
        }

        private void onShowing(object sender, EventArgs e)
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    LiveFlipPanel_1.Visible = false;
                    break;

                default:
                    LiveFlipPanel_1.Visible = false;
                    break;
            }
        }

        private void onShown(object sender, EventArgs e)
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    new BunjeeJumpEffect()
                    {
                        Widget = LiveFlipPanel_1,
                    }.Start();
                    break;

                default:
                    new BunjeeJumpEffect()
                    {
                        Widget = LiveFlipPanel_1,
                    }.Start();
                    break;
            }
        }

    }
}
