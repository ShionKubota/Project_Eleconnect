// AUTOMATICALLY GENERATED CODE

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Imaging;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.HighLevel.UI;

namespace Preview
{
    partial class EditUI
    {
        Label TitleLabel;
        Button ChangePanelBuuton;
        Button SetSwitchButton;
        Button LoadButton;
        PopupList LoadNoList;
        Slider MapWidthSlider;
        Button SetStartButton;
        Button SetGoalButton;
        Button SaveButton;
        Button RandomSetButton;

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
            SetStartButton = new Button();
            SetStartButton.Name = "SetStartButton";
            SetGoalButton = new Button();
            SetGoalButton.Name = "SetGoalButton";
            SaveButton = new Button();
            SaveButton.Name = "SaveButton";
            RandomSetButton = new Button();
            RandomSetButton.Name = "RandomSetButton";

            // EditUI
            this.RootWidget.AddChildLast(TitleLabel);
            this.RootWidget.AddChildLast(ChangePanelBuuton);
            this.RootWidget.AddChildLast(SetSwitchButton);
            this.RootWidget.AddChildLast(LoadButton);
            this.RootWidget.AddChildLast(LoadNoList);
            this.RootWidget.AddChildLast(MapWidthSlider);
            this.RootWidget.AddChildLast(SetStartButton);
            this.RootWidget.AddChildLast(SetGoalButton);
            this.RootWidget.AddChildLast(SaveButton);
            this.RootWidget.AddChildLast(RandomSetButton);
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
            LoadNoList.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            LoadNoList.Font = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            LoadNoList.ListItemTextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            LoadNoList.ListItemFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            LoadNoList.ListTitleTextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            LoadNoList.ListTitleFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);

            // MapWidthSlider
            MapWidthSlider.MinValue = 2;
            MapWidthSlider.MaxValue = 8;
            MapWidthSlider.Value = 2;
            MapWidthSlider.Step = 1;

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

            // RandomSetButton
            RandomSetButton.TextColor = new UIColor(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);
            RandomSetButton.TextFont = new UIFont(FontAlias.System, 25, FontStyle.Regular);
            RandomSetButton.BackgroundFilterColor = new UIColor(0f / 255f, 124f / 255f, 255f / 255f, 255f / 255f);

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

                    RandomSetButton.SetPosition(713, 378);
                    RandomSetButton.SetSize(214, 56);
                    RandomSetButton.Anchors = Anchors.None;
                    RandomSetButton.Visible = true;

                    break;

                default:
                    this.DesignWidth = 960;
                    this.DesignHeight = 544;

                    TitleLabel.SetPosition(325, 474);
                    TitleLabel.SetSize(342, 54);
                    TitleLabel.Anchors = Anchors.None;
                    TitleLabel.Visible = true;

                    ChangePanelBuuton.SetPosition(152, 21);
                    ChangePanelBuuton.SetSize(124, 55);
                    ChangePanelBuuton.Anchors = Anchors.None;
                    ChangePanelBuuton.Visible = true;

                    SetSwitchButton.SetPosition(325, 21);
                    SetSwitchButton.SetSize(137, 55);
                    SetSwitchButton.Anchors = Anchors.None;
                    SetSwitchButton.Visible = true;

                    LoadButton.SetPosition(30, 470);
                    LoadButton.SetSize(122, 56);
                    LoadButton.Anchors = Anchors.None;
                    LoadButton.Visible = true;

                    LoadNoList.SetPosition(174, 470);
                    LoadNoList.SetSize(177, 56);
                    LoadNoList.Anchors = Anchors.Height;
                    LoadNoList.Visible = true;

                    MapWidthSlider.SetPosition(729, 412);
                    MapWidthSlider.SetSize(195, 58);
                    MapWidthSlider.Anchors = Anchors.Height;
                    MapWidthSlider.Visible = true;

                    SetStartButton.SetPosition(511, 21);
                    SetStartButton.SetSize(135, 55);
                    SetStartButton.Anchors = Anchors.None;
                    SetStartButton.Visible = true;

                    SetGoalButton.SetPosition(695, 21);
                    SetGoalButton.SetSize(135, 55);
                    SetGoalButton.Anchors = Anchors.None;
                    SetGoalButton.Visible = true;

                    SaveButton.SetPosition(30, 378);
                    SaveButton.SetSize(122, 56);
                    SaveButton.Anchors = Anchors.None;
                    SaveButton.Visible = true;

                    RandomSetButton.SetPosition(744, 470);
                    RandomSetButton.SetSize(165, 56);
                    RandomSetButton.Anchors = Anchors.None;
                    RandomSetButton.Visible = true;

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

            RandomSetButton.Text = "ランダムセット";
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
                    SetStartButton.Visible = false;
                    SetGoalButton.Visible = false;
                    SaveButton.Visible = false;
                    RandomSetButton.Visible = false;
                    break;

                default:
                    TitleLabel.Visible = false;
                    ChangePanelBuuton.Visible = false;
                    SetSwitchButton.Visible = false;
                    LoadButton.Visible = false;
                    LoadNoList.Visible = false;
                    MapWidthSlider.Visible = false;
                    SetStartButton.Visible = false;
                    SetGoalButton.Visible = false;
                    SaveButton.Visible = false;
                    RandomSetButton.Visible = false;
                    break;
            }
        }

        private void onShown(object sender, EventArgs e)
        {
            switch (_currentLayoutOrientation)
            {
                case LayoutOrientation.Vertical:
                    new SlideInEffect()
                    {
                        Widget = TitleLabel,
                        MoveDirection = FourWayDirection.Up,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = ChangePanelBuuton,
                        MoveDirection = FourWayDirection.Left,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = SetSwitchButton,
                        MoveDirection = FourWayDirection.Left,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = LoadButton,
                        MoveDirection = FourWayDirection.Right,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = LoadNoList,
                        MoveDirection = FourWayDirection.Right,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = MapWidthSlider,
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
                        MoveDirection = FourWayDirection.Right,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = RandomSetButton,
                        MoveDirection = FourWayDirection.Right,
                    }.Start();
                    break;

                default:
                    new SlideInEffect()
                    {
                        Widget = TitleLabel,
                        MoveDirection = FourWayDirection.Up,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = ChangePanelBuuton,
                        MoveDirection = FourWayDirection.Left,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = SetSwitchButton,
                        MoveDirection = FourWayDirection.Left,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = LoadButton,
                        MoveDirection = FourWayDirection.Right,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = LoadNoList,
                        MoveDirection = FourWayDirection.Right,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = MapWidthSlider,
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
                        MoveDirection = FourWayDirection.Right,
                    }.Start();
                    new SlideInEffect()
                    {
                        Widget = RandomSetButton,
                        MoveDirection = FourWayDirection.Right,
                    }.Start();
                    break;
            }
        }

    }
}
