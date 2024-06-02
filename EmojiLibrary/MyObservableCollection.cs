using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EmojiLibrary
{
    // Делегат для обработки событий изменения коллекции
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    // Аргументы для событий изменения коллекции
    public class CollectionHandlerEventArgs : EventArgs
    {
        public string ChangeType { get; set; }
        public object Item { get; set; }

        public CollectionHandlerEventArgs(string changeType, object item)
        {
            ChangeType = changeType;
            Item = item;
        }
    }

    // Коллекция с уведомлениями об изменениях
    namespace EmojiLibrary
    {
        public class MyObservableCollection<T> : MyCollection<T> where T : IInit, ICloneable, new()
        {
            public event CollectionHandler CollectionCountChanged;
            public event CollectionHandler CollectionReferenceChanged;

            private string collectionName;

            public MyObservableCollection(string name)
            {
                collectionName = name;
            }

            protected virtual void OnCollectionCountChanged(string changeType, T item)
            {
                CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs(changeType, item));
            }

            protected virtual void OnCollectionReferenceChanged(string changeType, T item)
            {
                CollectionReferenceChanged?.Invoke(this, new CollectionHandlerEventArgs(changeType, item));
            }

            public override void Add(T item)
            {
                base.Add(item);
                OnCollectionCountChanged("Item added", item);
            }

            public override bool Remove(T item)
            {
                bool result = base.Remove(item);
                if (result)
                {
                    OnCollectionCountChanged("Item removed", item);
                }
                return result;
            }

            public override void Insert(int index, T item)
            {
                base.Insert(index, item);
                OnCollectionCountChanged("Item inserted", item);
            }

            public override void RemoveAt(int index)
            {
                T removedItem = this[index];
                base.RemoveAt(index);
                OnCollectionCountChanged("Item removed at index", removedItem);
            }

            public override T this[int index]
            {
                get => base[index];
                set
                {
                    T oldItem = base[index];
                    base[index] = value;
                    OnCollectionReferenceChanged("Item replaced", value);
                }
            }
        }
    }
}
