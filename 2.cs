using System;

namespace RocketLaunchSimulator
{
    // Rocket State Interface
    public interface IRocketState
    {
        void Handle(Rocket rocket);
    }

    // Concrete States
    public class PreLaunchState : IRocketState
    {
        public void Handle(Rocket rocket)
        {
            Console.WriteLine("Stage: Pre-Launch");
            Console.WriteLine("All systems are 'Go' for launch.");
            rocket.SetState(new Stage1State());
        }
    }

    public class Stage1State : IRocketState
    {
        public void Handle(Rocket rocket)
        {
            if (rocket.Fuel <= 0)
            {
                rocket.SetState(new MissionFailureState());
            }
            else
            {
                rocket.Fuel -= 10;
                rocket.Altitude += 10;
                rocket.Speed += 1000;
                Console.WriteLine($"Stage: 1, Fuel: {rocket.Fuel}%, Altitude: {rocket.Altitude} km, Speed: {rocket.Speed} km/h");
                if (rocket.Fuel <= 50)
                {
                    rocket.SetState(new Stage2State());
                }
            }
        }
    }

    public class Stage2State : IRocketState
    {
        public void Handle(Rocket rocket)
        {
            if (rocket.Fuel <= 0)
            {
                rocket.SetState(new MissionFailureState());
            }
            else
            {
                rocket.Fuel -= 5;
                rocket.Altitude += 20;
                rocket.Speed += 2000;
                Console.WriteLine($"Stage: 2, Fuel: {rocket.Fuel}%, Altitude: {rocket.Altitude} km, Speed: {rocket.Speed} km/h");
                if (rocket.Altitude >= 100)
                {
                    rocket.SetState(new OrbitState());
                }
            }
        }
    }

    public class OrbitState : IRocketState
    {
        public void Handle(Rocket rocket)
        {
            Console.WriteLine("Orbit achieved! Mission Successful.");
        }
    }

    public class MissionFailureState : IRocketState
    {
        public void Handle(Rocket rocket)
        {
            Console.WriteLine("Mission Failed due to insufficient fuel.");
        }
    }

    // Rocket Class
    public class Rocket
    {
        public int Fuel { get; set; } = 100;
        public int Altitude { get; set; } = 0;
        public int Speed { get; set; } = 0;

        private IRocketState _currentState;

        public Rocket()
        {
            _currentState = new PreLaunchState();
        }

        public void SetState(IRocketState state)
        {
            _currentState = state;
        }

        public void Update()
        {
            _currentState.Handle(this);
        }
    }

    // Command Pattern Interface
    public interface ICommand
    {
        void Execute(Rocket rocket);
    }

    // Concrete Commands
    public class StartChecksCommand : ICommand
    {
        public void Execute(Rocket rocket)
        {
            rocket.Update();
        }
    }

    public class LaunchCommand : ICommand
    {
        public void Execute(Rocket rocket)
        {
            for (int i = 0; i < 10; i++)
            {
                rocket.Update();
            }
        }
    }

    public class FastForwardCommand : ICommand
    {
        private int _seconds;

        public FastForwardCommand(int seconds)
        {
            _seconds = seconds;
        }

        public void Execute(Rocket rocket)
        {
            for (int i = 0; i < _seconds; i++)
            {
                rocket.Update();
            }
        }
    }

    // User Input Simulation
    class Program
    {
        static void Main(string[] args)
        {
            Rocket rocket = new Rocket();
            ICommand startChecksCommand = new StartChecksCommand();
            ICommand launchCommand = new LaunchCommand();
            
            Console.WriteLine("Type 'start_checks' to initiate system checks or 'launch' to start the mission.");

            while (true)
            {
                var input = Console.ReadLine();
                if (input == "start_checks")
                {
                    startChecksCommand.Execute(rocket);
                }
                else if (input == "launch")
                {
                    launchCommand.Execute(rocket);
                }
                else if (input.StartsWith("fast_forward"))
                {
                    var splitInput = input.Split(' ');
                    if (splitInput.Length == 2 && int.TryParse(splitInput[1], out int seconds))
                    {
                        ICommand fastForwardCommand = new FastForwardCommand(seconds);
                        fastForwardCommand.Execute(rocket);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid command.");
                }
            }
        }
    }
}
