using System.Diagnostics;
using System.Collections;
using System;
using EmojiLibrary;
using System.Diagnostics.CodeAnalysis;
using EmojiLibrary.EmojiLibrary;

namespace la12
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Создаем две коллекции MyObservableCollection
            MyObservableCollection<Emoji> collection1 = new MyObservableCollection<Emoji>("Collection 1");
            MyObservableCollection<Emoji> collection2 = new MyObservableCollection<Emoji>("Collection 2");

            // Создаем два объекта Journal
            Journal journal1 = new Journal();
            Journal journal2 = new Journal();

            // Подписываем journal1 на события CollectionCountChanged и CollectionReferenceChanged из первой коллекции
            collection1.CollectionCountChanged += (source, args) =>
            {
                journal1.AddEntry(new JournalEntry("Collection 1", args.ChangeType, args.Item.ToString()));
            };
            collection1.CollectionReferenceChanged += (source, args) =>
            {
                journal1.AddEntry(new JournalEntry("Collection 1", args.ChangeType, args.Item.ToString()));
            };

            // Подписываем journal2 на события CollectionReferenceChanged из обеих коллекций
            collection1.CollectionReferenceChanged += (source, args) =>
            {
                journal2.AddEntry(new JournalEntry("Collection 1", args.ChangeType, args.Item.ToString()));
            };
            collection2.CollectionReferenceChanged += (source, args) =>
            {
                journal2.AddEntry(new JournalEntry("Collection 2", args.ChangeType, args.Item.ToString()));
            };

            // Вносим изменения в коллекции
            Emoji emoji1 = new Emoji("Smile", "<smile>");
            Emoji emoji2 = new Emoji("Happy", "<happy>");
            Emoji emoji3 = new Emoji("Thinking", "<thinking>");
            Emoji emoji4 = new Emoji("Wave", "<wave>");
            Emoji emoji5 = new Emoji("Celebrate", "<celebrate>");

            collection1.Add(emoji1);
            collection1.Add(emoji2);
            collection1[0] = emoji3;
            collection1.Remove(emoji2);

            collection2.Add(emoji4);
            collection2[0] = emoji5;

            // Вывод данных обоих объектов Journal
            Console.WriteLine("Journal 1:");
            Console.WriteLine(journal1);

            Console.WriteLine("\nJournal 2:");
            Console.WriteLine(journal2);
        }
    }
}