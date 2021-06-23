using Newtonsoft.Json;
using Somerpg.Client.Actions;
using Somerpg.Execution;
using Somerpg.Common.Model;
using Somerpg.Client.Util;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;

namespace Somerpg.Client.Service
{
    class DummyService : IService
    {
        private static readonly TimeSpan INTERVAL = TimeSpan.FromSeconds(1);
        private readonly GameActionStore _actionStore = new GameActionStore();
        private IObserver<IGameAction> _observer;
        private IDisposable _timer;

        public void Subscribe(Action<IGameAction> action_) 
        {
            _observer = Observer.Create(action_);
            _timer = Observable.Interval(INTERVAL)
                .Subscribe(OnTick);
        }

        private void OnTick(long _)
        {
            _actionStore.Tick();
            var finishedAction = _actionStore.TryPopLastFinishedAction();
            if (finishedAction != null)
            {
                _observer.OnNext(ProgressWithAction(finishedAction));
            }
        }

        private IGameAction ProgressWithAction(IGameAction gameAction_)
        {
            Player newPlayer;
            switch (gameAction_.Action)
            {
                case DungeonAction action:
                    var rewards = new DungeonExecutor().Execute(action, gameAction_.Player);
                    action.Rewards = rewards;
                    newPlayer = gameAction_.Player.WithRewards(rewards);
                    break;
                case AddXPAction action:
                    newPlayer = gameAction_.Player.WithXP(action.XPToAdd);
                    break;
                default:
                    throw new ArgumentException();
            }
            gameAction_.Player = newPlayer;
            return gameAction_;
        }
            
        public TestModel StartAction(TestModel model_)
        {
            // todo connect to localhost:8080
            var json = JsonConvert.SerializeObject(model_);

            using (var client = new HttpClient())
            {
 
                // List data response.
                var response = client.PostAsync("http://localhost:8080", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = response.Content.ReadAsStringAsync().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    var resultModel = JsonConvert.DeserializeObject<TestModel>(responseJson);
                    return resultModel;
                }
            }

            return model_;
        }

        public IGameAction StartAction(Player player_, IAction action_)
        {
            var gameAction = new GameAction
            {
                Player = player_,
                Action = action_,
                Description = GetDescription(action_),
                TimeLeft = GetTimeLeft(player_, action_)
            };

            _actionStore.Add(gameAction);

            // to prevent shared references, this is a dummy class anyway, imagine this will be on a server
            return new GameAction
            {
                Player = player_,
                Action = action_,
                Description = gameAction.Description,
                TimeLeft = gameAction.TimeLeft
            };

        }

        private string GetDescription(IAction action_)
        {
            return action_ switch
            {
                AddXPAction _ => "Leveling up...",
                DungeonAction a => $"Doing T{a.Tier} dungeon...",
                _ => throw new ArgumentException(),
            };
        }

        private long GetTimeLeft(Player player_, IAction action_)
        {
            return 5;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
