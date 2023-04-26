using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    internal class Dictionary<TKey, TValue> where TKey : IEquatable<TKey>
    {
        private const int DefaultCapacity = 1000;
        private Entry[] table;
        public Dictionary() 
        { 
            table = new Entry[DefaultCapacity]; // Dictionary가 생성되면 1000개의 엔트리 배열 생성
        }
        private struct Entry
        {
            internal TKey key;
            internal TValue value;

            internal enum EntryState { None, Using, Deleted}
            internal EntryState state;
        }
        /// <summary>
        /// get: 키에 해당하는 값이 있다면 값 반환 없다면 에러
        /// set: 키에 해당하는 값이 있다면 값 변경 없다면 에러
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public TValue this[TKey key]
        {
            get 
            {
                TValue value;
                if(TryGetValue(key,out value))
                {
                    return value;
                }
                else{ throw new InvalidOperationException(); } // 없는 값 접근 에러
            }
            set
            {
                int index = FindIndex(key);
                if(index == -1) { throw new InvalidOperationException(); }
                table[index].value = value;
            }
        }
        public void Add(TKey key, TValue value)
        {
            int index = FindIndex(key);
            if (index != -1) throw new InvalidOperationException(); // 중복된 키라면 에러

            index = Math.Abs(key.GetHashCode()) % table.Length;
            while (table[index].state.Equals(Entry.EntryState.Using)) // 쓰고있는 칸이 아닐 때까지 반복
            {
                index = (index * index) % table.Length;
            }
            table[index].key = key;
            table[index].value = value;
            table[index].state = Entry.EntryState.Using;
        }
        /// <summary>
        /// 일치하는 키가 있다면 out에 value 저장후 true 리턴 없다면 value에 default 저장 후 false 반환
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value) 
        {
            int index = FindIndex(key);
            if(index == -1)
            {
                value = default;
                return false;
            }
            value = table[index].value;
            return true;
        }
        public bool Remove(TKey key)
        {
            int index = FindIndex(key);
            if(index == -1) return false; // 없는 값 제거 시 실패 리턴
            table[index].state = Entry.EntryState.Deleted;
            return true;
        }
        /// <summary>
        /// 일치하는 key가 있다면 index 반환 없다면 -1 반환
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int FindIndex(TKey key)
        {
            int index = Math.Abs(key.GetHashCode()) % table.Length;
            while (table[index].state != Entry.EntryState.None) // None이 아닌동안 탐색
            {
                if (key.Equals(table[index].key))
                {
                    // 만약 찾은 키가 Deleted 키 일 경우 -1 리턴
                    if (table[index].state == Entry.EntryState.Deleted) return -1;
                    return index;
                }
                index = (index * index) % table.Length;
            }
            return -1;
        }

    }
}
