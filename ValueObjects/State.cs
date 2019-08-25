using System.Collections.Generic;

namespace AFD.ValueObjects
{
    public class StateValueObject
    {
        private Dictionary<char, StateValueObject> transition;
        private bool marcado;
        private int identificador;

        public StateValueObject(bool marcado, int identificador)
        {
            this.marcado = marcado;
            this.identificador = identificador;
            this.transition = new Dictionary<char, StateValueObject>();
        }
        public void addTransition(char c, StateValueObject estado) => transition.Add(c, estado);
        public StateValueObject getNextState(char c) => transition.GetValueOrDefault(c);
        public string toString() => $"S{identificador}";
        public bool isChecked() => marcado;
    }
}