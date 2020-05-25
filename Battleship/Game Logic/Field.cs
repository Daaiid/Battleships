using System.ComponentModel;
using System.Runtime.CompilerServices;
using Battleship.Annotations;
using Battleship.Enum;

namespace Battleship.Game_Logic
{
    public class Field : INotifyPropertyChanged
    {
        private FieldState _state;

        public Field(FieldState initialState = FieldState.Empty)
        {
            if (initialState == FieldState.Empty)
            {
                IsChangeable = true;
            }
            else
            {
                IsChangeable = false;
            }

            State = initialState;
        }


        public bool IsChangeable { get; }

        public FieldState State
        {
            get { return _state; }
            set
            {
                if (IsChangeable)
                {
                    _state = value;
                    OnPropertyChanged();
                }
            }
        }

        public void CycleFieldState()
        {
            switch (_state)
            {
                case FieldState.Empty:
                    State = FieldState.Water;
                    break;

                case FieldState.Water:
                    State = FieldState.Ship;
                    break;

                case FieldState.Ship:
                    State = FieldState.Empty;
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
