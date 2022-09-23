using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.Domain.Contracts
{
    // https://matheus.ro/2017/09/26/design-patterns-and-practices-in-net-option-functional-type-in-csharp/
    public class Option<T> : IEnumerable<T>
    {
        private readonly T[] _data;

        private Option(T[] data)
        {
            _data = data;
        }

        public static Option<T> Create(T element)
        {
            return new Option<T>(new T[] { element });
        }

        public static Option<T> CreateEmpty()
        {
            return new Option<T>(Array.Empty<T>());
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
