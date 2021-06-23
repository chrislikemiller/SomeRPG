using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Somerpg.Client.Actions;
using Somerpg.Common.Model;
using Somerpg.Client.Util;
using Somerpg.Common.Util;
using Somerpg.View;
using Somerpg.Client.Service;

namespace Somerpg.Client.ViewModel
{
    public class MainViewModel : NotifyBase
    {
        private const string NO_ACTION_INPROGRESS = "None";

        private readonly GameActionStore _actionStore;
        private readonly IService _service;
        private readonly ITimerService _timerService;
        private IGameAction _currentAction;
        private Player _player;
        private IDisposable _timer;
        private bool _isActionInProgress;
        private bool _isToolWindowOpen;

        public ICommand AddXPCommand { get; set; }
        public ICommand DoDungeonCommand { get; set; }
        public ICommand ManageInventoryCommand { get; set; }
        public ICommand CraftItemsCommand { get; set; }

        public bool IsToolWindowOpen
        {
            get => _isToolWindowOpen;
            set
            {
                _isToolWindowOpen = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> Logs { get; } = new ObservableCollection<string>();

        public IGameAction CurrentAction
        {
            get => _currentAction;
            set
            {
                _currentAction = value;
                OnPropertyChanged();
            }
        }

        public bool IsActionInProgress
        {
            get => _isActionInProgress;
            set
            {
                _isActionInProgress = value;
                OnPropertyChanged();
            }
        }

        public Player Player
        {
            get => _player;
            set
            {
                _player = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(IService service, ITimerService timerService)
        {
            _player = new Player();
            _actionStore = new GameActionStore();

            _service = service;
            _timerService = timerService;
            _service.Subscribe(OnActionFinished);
            
            CurrentAction = new GameAction { Description = NO_ACTION_INPROGRESS };
            AddXPCommand = new Command(() => AddNewAction(_service.StartAction(Player, new AddXPAction { XPToAdd = 1000 })));
            DoDungeonCommand = new Command<string>(tier => AddNewAction(_service.StartAction(Player, new DungeonAction { Tier = int.Parse(tier) })));
            ManageInventoryCommand = new Command(() =>
            {
                var invViewModel = new InventoryViewModel(Player.Copy());
                var window = new InventoryWindow { DataContext = invViewModel };
                window.Closed += (s, e) =>
                {
                    Player.Armor = invViewModel.Player.Armor;
                    Player.Weapon = invViewModel.Player.Weapon;
                    //Player.Inventory.Materials = invViewModel.Player.Inventory.Materials;
                    //Player.Inventory.Items.ClearAndReplace(invViewModel.Player.Inventory.Items);
                    Player.Inventory.Replace(invViewModel.Player.Inventory);
                    OnPropertyChanged(nameof(Player));
                    IsToolWindowOpen = false;
                };
                IsToolWindowOpen = true;
                window.ShowDialog();
            });
            CraftItemsCommand = new Command(() =>
            {
                var invViewModel = new CraftViewModel(Player.Copy());
                var window = new CraftWindow() { DataContext = invViewModel };
                window.Closed += (s, e) =>
                {
                    Player.Inventory.Replace(invViewModel.Player.Inventory);
                    IsToolWindowOpen = false;
                };
                IsToolWindowOpen = true;
                window.ShowDialog();
            });
        }

        private void AddNewAction(IGameAction gameAction)
        {
            IsActionInProgress = true;
            CurrentAction = gameAction;
            _actionStore.Add(gameAction);

            // makes us quicker by 1 second compared to server 
            // this is to ensure that we are "ahead" of the server, in case OnTimerTick never runs for any reason
            _actionStore.Tick();

            _timer = _timerService.StartNew(OnTimerTick);
        }

        private void OnTimerTick(long tick)
        {
            OnPropertyChanged(nameof(CurrentAction));
            _actionStore.Tick();
        }

        private void OnActionFinished(IGameAction result)
        {
            _timer?.Dispose();
            _actionStore.Tick();
            var _ = _actionStore.TryPopLastFinishedAction();

            StopAction(result);
        }

        private void StopAction(IGameAction result)
        {
            CurrentAction.Description = NO_ACTION_INPROGRESS;
            CurrentAction.TimeLeft = -1;
            var hasLeveledUp = Player.Level < result.Player.Level;
            Player = result.Player;

            var logMessage = result.Action switch
            {
                AddXPAction a => $"You got {a.XPToAdd} XP!",
                DungeonAction a => $"Tier {a.Tier} dungeon finished! You got {a.Rewards.XP} XP, {a.Rewards.Inventory.Gold} gold and {a.Rewards.Inventory.Items.Count} items!",
                _ => throw new ArgumentException()
            };
            Log(logMessage);
            if (hasLeveledUp)
            {
                Log($"You leveled up to {result.Player.Level}");
            }

            IsActionInProgress = false;
            OnPropertyChanged(nameof(CurrentAction));
        }

        private void Log(string logMessage)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Logs.Add($"[{DateTime.Now.ToLongTimeString()}] {logMessage}");
            });
        }

        private string GetLogMessageForAction(IAction action_)
        {
            return action_ switch
            {
                AddXPAction _ => $"[{DateTime.Now.ToLongTimeString()}] You got XP!",
                AddGoldAction _ => $"[{DateTime.Now.ToLongTimeString()}] You got gold!",
                _ => throw new ArgumentException()
            };
        }
    }
}
