// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Eleconnect
{
    partial class EditUI
    {
        Label TitleLabel;
        Button ChangePanelBuuton;
        Button SetSwitchButton;
        Button LoadButton;
        PopupList LoadNoList;
        Slider MapWidthSlider;
        Slider MapHeightSlider;
        Button SetStartButton;
        Button SetGoalButton;
        Button SaveButton;

        private void InitializeWidget()
        {
            InitializeWidget(LayoutOrientation.Horizontal);
        }

        private void InitializeWidget(LayoutOrientation orientation)
        {
            TitleLabel = new Label();
            TitleLabel.Name = "TitleLabel";
            ChangePanelBuuton = new Button();
            ChangePanelBuuton.Name = "ChangePanelBuuton";
            SetSwitchButton = new Button();
            SetSwitchButton.Name = "SetSwitchButton";
            LoadButton = new Button();
            LoadButton.Name = "LoadButton";
            LoadNoList = new PopupList();
            LoadNoList.Name = "LoadNoList";
            MapWidthSlider = new Slider();
            MapWidthSlider.Name = "MapWidthSlider";
            MapHeightSlider = new Slider();
            MapHeightSlider.Name = "MapHeightSlider";
            SetStartButton = new Button();
            SetStartButton.Name = "SetStartButton";
            SetGoalButton = new Button();
            SetGoalButton.Name = "SetGoalButton";
            SaveButton = new Button();
            SaveButton.Name = "SaveButton";

            // EditUI
            this.RootWidget.AddChildLast(TitleLabel);
            this.RootWidget.AddChildLast(ChangePanelBuuton);
            this.RootWidget.AddChildLast(SetSwitchButton);
            this.RootWidget.AddChildLast(LoadButton);
            this.RootWidget.AddChildLast(LoadNoList);
            this.RootWidget.AddChildLast(MapWidthSlider);
            this.RootWidget.AddChildLast(MapHeightSlider);
            this.RootWidget.AddChildLast(SetStartButton);
            this.RootWidget.AddChildLast(SetGoalButton);
            this.RootWidget.AddChildLast(SaveButton);
            this.Showing += new EventHandler(onShowing);
            this.Shown += new EventHandler(onShown);

            // TitleLabel
            TitleLabel.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            TitleLabel.Font = new UIFont(FontAlias.System, 28, FontStyle.Bold | FontStyle.Italic);
            TitleLabel.LineBreak = LineBreak.Character;
            TitleLabel.HorizontalAlignment = HorizontalAlignment.Center;

            // ChangePanelBuuton
            ChangePanelBuuton.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            ChangePanelBuuton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);

            // SetSwitchButton
            SetSwitchButton.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            SetSwitchButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);

            // LoadButton
            LoadButton.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            LoadButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            LoadButton.BackgroundFilterColor = new UIColor(0f / 255f, 222f / 255f, 38f / 255f, 255f / 255f);

            // LoadNoList
            LoadNoList.TextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            LoadNoList.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            LoadNoList.ListItemTextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            LoadNoList.ListItemFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            LoadNoList.ListTitleTextColor = new UIColor(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            LoadNoList.ListTitleFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);

            // MapWidthSlider
            MapWidthSlider.MinValue = 2;
            MapWidthSlider.MaxValue = 9;
            MapWidthSlider.Value = 2;
            MapWidthSlider.Step = 1;

            // MapHeightSlider
            MapHeightSlider.Orientation = SliderOrientation.Vertical;
            MapHeightSlider.MinValue = 2;
            MapHeightSlider.MaxValue = 9;
            MapHeightSlider.Value = 2;
            MapHeightSlider.Step = 1;

            // SetStartButton
            SetStartButton.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            SetStartButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);

            // SetGoalButton
            SetGoalButton.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            SetGoalButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);

            // SaveButton
            SaveButton.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            SaveButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            SaveButton.BackgroundFilterColor = new UIColor(255f / 255f, 0f / 255f, 7f / 255f, 255f / 255f);

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

                    TitleLabel.SetPosition(361, 37);
                    TitleLabel.SetSize(214, 36);
                    TitleLabel.Anchors = Anchors.None;
                    TitleLabel.Visible = true;

                    ChangePanelBuuton.SetPosition(21, 139);
                    ChangePanelBuuton.SetSize(214, 56);
                    ChangePanelBuuton.Anchors = Anchors.None;
                    ChangePanelBuuton.Visible = true;

                    SetSwitchButton.SetPosition(43, 184);
                    SetSwitchButton.SetSize(214, 56);
                    SetSwitchButton.Anchors = Anchors.None;
                    SetSwitchButton.Visible = true;

                    LoadButton.SetPosition(43, 395);
                    LoadButton.SetSize(214, 56);
                    LoadButton.Anchors = Anchors.None;
                    LoadButton.Visible = true;

                    LoadNoList.SetPosition(414, 470);
                    LoadNoList.SetSize(360, 56);
                    LoadNoList.Anchors = Anchors.Height;
                    LoadNoList.Visible = true;

                    MapWidthSlider.SetPosition(43, 331);
                    MapWidthSlider.SetSize(362, 58);
                    MapWidthSlider.Anchors = Anchors.Height;
                    MapWidthSlider.Visible = true;

                    MapHeightSlider.SetPosition(43, 331);
                    MapHeightSlider.SetSize(362, 58);
                    MapHeightSlider.Anchors = Anchors.Height;
                    MapHeightSlider.Visible = true;

                    SetStartButton.SetPosition(702, 57);
                    SetStartButton.SetSize(214, 56);
                    SetStartButton.Anchors = Anchors.None;
                    SetStartButton.Visible = true;

                    SetGoalButton.SetPosition(786, 177);
                    SetGoalButton.SetSize(214, 56);
                    SetGoalButton.Anchors = Anchors.None;
                    SetGoalButton.Visible = true;

                    SaveButton.SetPosition(43, 395);
                    SaveButton.SetSize(214, 56);
                    SaveButton.Anchors = Anchors.None;
                    SaveButton.Visible = true;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    TitleLabel.SetPosition(309, 20);
                    TitleLabel.SetSize(342, 54);
                    TitleLabel.Anchors = Anchors.None;
                    TitleLabel.Visible = true;

                    ChangePanelBuuton.SetPosition(43, 67);
                    ChangePanelBuuton.SetSize(124, 73);
                    ChangePanelBuuton.Anchors = Anchors.None;
                    ChangePanelBuuton.Visible = true;

                    SetSwitchButton.SetPosition(43, 177);
                    SetSwitchButton.SetSize(137, 72);
                    SetSwitchButton.Anchors = Anchors.None;
                    SetSwitchButton.Visible = true;

                    LoadButton.SetPosition(814, 389);
                    LoadButton.SetSize(122, 56);
                    LoadButton.Anchors = Anchors.None;
                    LoadButton.Visible = true;

                    LoadNoList.SetPosition(608, 389);
                    LoadNoList.SetSize(177, 56);
                    LoadNoList.Anchors = Anchors.Height;
                    LoadNoList.Visible = true;

                    MapWidthSlider.SetPosition(101, 468);
                    MapWidthSlider.SetSize(195, 58);
                    MapWidthSlider.Anchors = Anchors.Height;
                    MapWidthSlider.Visible = true;

                    MapHeightSlider.SetPosition(43, 275);
                    MapHeightSlider.SetSize(58, 195);
                    MapHeightSlider.Anchors = Anchors.Height;
                    MapHeightSlider.Visible = true;

                    SetStartButton.SetPosition(786, 157);
                    SetStartButton.SetSize(135, 73);
                    SetStartButton.Anchors = Anchors.None;
                    SetStartButton.Visible = true;

                    SetGoalButton.SetPosition(786, 266);
                    SetGoalButton.SetSize(135, 73);
                    SetGoalButton.Anchors = Anchors.None;
                    SetGoalButton.Visible = true;

                    SaveButton.SetPosition(814, 470);
                    SaveButton.SetSize(122, 56);
                    SaveButton.Anchors = Anchors.None;
                    SaveButton.Visible = true;

                    break;
            }
            _currentLayoutOrientation = orientation;
        }

        public void UpdateLanguage()
        {
            this.Title = "EditUI";

            TitleLabel.Text = "EditMode";

            ChangePanelBuuton.Text = "パネル変更";

            SetSwitchButton.Text = "スイッチ設置";

            LoadButton.Text = "読込み";

            LoadNoList.ListTitle = "読み込むファイルNo";
            LoadNoList.ListItems.Clear();
            LoadNoList.ListItems.AddRange(new String[]
            {
                "no selected",
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
            });
            LoadNoList.SelectedIndex = 0;

            SetStartButton.Text = "スタート設置";

            SetGoalButton.Text = "ゴール設置";

            SaveButton.Text = "保存";
        }

        private void onShowing(object sender, EventArgs e)
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    TitleLabel.Visible = false;
                    ChangePanelBuuton.Visible = false;
                    SetSwitchButton.Visible = false;
                    LoadButton.Visible = false;
                    LoadNoList.Visible = false;
                    MapWidthSlider.Visible = false;
                    MapHeightSlider.Visible = false;
                    SetStartButton.Visible = false;
                    SetGoalButton.Visible = false;
                    SaveButton.Visible = false;
                    break;

                default:
                    TitleLabel.Visible = false;
                    ChangePanelBuuton.Visible = false;
                    SetSwitchButton.Visible = false;
                    LoadButton.Visible = false;
                    LoadNoList.Visible = false;
                    MapWidthSlider.Visible = false;
                    MapHeightSlider.Visible = false;
                    SetStartButton.Visible = false;
                    SetGoalButton.Visible = false;
                    SaveButton.Visible = false;
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
                        Widget = TitleLabel,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = ChangePanelBuuton,
                        MoveDirection = FourWayDirection.Right,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = SetSwitchButton,
                        MoveDirection = FourWayDirection.Right,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = LoadButton,
                        MoveDirection = FourWayDirection.Left,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = LoadNoList,
                        MoveDirection = FourWayDirection.Left,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = MapWidthSlider,
                        MoveDirection = FourWayDirection.Up,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = MapHeightSlider,
                        MoveDirection = FourWayDirection.Right,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = SetStartButton,
                        MoveDirection = FourWayDirection.Left,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = SetGoalButton,
                        MoveDirection = FourWayDirection.Left,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = SaveButton,
                        MoveDirection = FourWayDirection.Up,
                    }.Start();
                    break;

                default:
                    new FadeInEffect()
                    {
                        Widget = TitleLabel,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = ChangePanelBuuton,
                        MoveDirection = FourWayDirection.Right,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = SetSwitchButton,
                        MoveDirection = FourWayDirection.Right,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = LoadButton,
                        MoveDirection = FourWayDirection.Left,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = LoadNoList,
                        MoveDirection = FourWayDirection.Left,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = MapWidthSlider,
                        MoveDirection = FourWayDirection.Up,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = MapHeightSlider,
                        MoveDirection = FourWayDirection.Right,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = SetStartButton,
                        MoveDirection = FourWayDirection.Left,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = SetGoalButton,
                        MoveDirection = FourWayDirection.Left,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = SaveButton,
                        MoveDirection = FourWayDirection.Up,
                    }.Start();
                    break;
            }
        }

    }
}
