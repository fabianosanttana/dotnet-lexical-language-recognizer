using System.Collections.Generic;
using System.Linq;

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
        public bool existsTransition(char c) {
            return transition.Keys.Where(obj => obj == c).Any();
        }
        public void addTransition(char c, StateValueObject estado) => transition.Add(c, estado);
        public StateValueObject getTransition(char c) => transition[transition.Keys.Where(obj => obj == c).First()];

        public StateValueObject getNextState(char c) => transition.GetValueOrDefault(c);
        public string toString() => $"S{identificador}";
        public bool isChecked() => marcado;
        public int getIdentifier() => identificador;

    }
}