using Battleship.Enum;

namespace Battleship.Game_Logic
{
    public class Field
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
                }
            }
        }
    }
}
