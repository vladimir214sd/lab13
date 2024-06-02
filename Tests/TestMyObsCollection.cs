using EmojiLibrary.EmojiLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;

namespace EmojiLibrary.Tests
{
    [TestClass]
    public class MyObservableCollectionTests
    {
        private MyObservableCollection<Emoji> collection;
        private bool eventTriggered;
        private CollectionHandlerEventArgs eventArgs;

        [TestInitialize]
        public void Setup()
        {
            collection = new MyObservableCollection<Emoji>("TestCollection");
            collection.CollectionCountChanged += Collection_CountChanged;
            collection.CollectionReferenceChanged += Collection_ReferenceChanged;
            eventTriggered = false;
            eventArgs = null;
        }

        private void Collection_CountChanged(object source, CollectionHandlerEventArgs args)
        {
            eventTriggered = true;
            eventArgs = args;
        }

        private void Collection_ReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            eventTriggered = true;
            eventArgs = args;
        }

        [TestMethod]
        public void Add_ShouldTriggerCollectionCountChangedEvent()
        {
            // Arrange
            var emoji = new Emoji("       ", "<happy>");

            // Act
            collection.Add(emoji);

            // Assert
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual("Item added", eventArgs.ChangeType);
            Assert.AreEqual(emoji, eventArgs.Item);
        }

        [TestMethod]
        public void Remove_ShouldTriggerCollectionCountChangedEvent()
        {
            // Arrange
            var emoji = new Emoji("       ", "<happy>");
            collection.Add(emoji);
            eventTriggered = false;

            // Act
            bool result = collection.Remove(emoji);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual("Item removed", eventArgs.ChangeType);
            Assert.AreEqual(emoji, eventArgs.Item);
        }

        [TestMethod]
        public void Insert_ShouldTriggerCollectionCountChangedEvent()
        {
            // Arrange
            var emoji = new Emoji("       ", "<happy>");

            // Act
            collection.Insert(0, emoji);

            // Assert
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual("Item inserted", eventArgs.ChangeType);
            Assert.AreEqual(emoji, eventArgs.Item);
        }

        [TestMethod]
        public void RemoveAt_ShouldTriggerCollectionCountChangedEvent()
        {
            // Arrange
            var emoji = new Emoji("       ", "<happy>");
            collection.Add(emoji);
            eventTriggered = false;

            // Act
            collection.RemoveAt(0);

            // Assert
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual("Item removed at index", eventArgs.ChangeType);
            Assert.AreEqual(emoji, eventArgs.Item);
        }

        [TestMethod]
        public void IndexerSet_ShouldTriggerCollectionReferenceChangedEvent()
        {
            // Arrange
            var oldEmoji = new Emoji("       ", "<happy>");
            var newEmoji = new Emoji("          ", "<thinking>");
            collection.Add(oldEmoji);
            eventTriggered = false;

            // Act
            collection[0] = newEmoji;

            // Assert
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual("Item replaced", eventArgs.ChangeType);
            Assert.AreEqual(newEmoji, eventArgs.Item);
        }

        [TestMethod]
        public void IndexerGet_ShouldReturnCorrectItem()
        {
            // Arrange
            var emoji = new Emoji("       ", "<happy>");
            collection.Add(emoji);

            // Act
            var result = collection[0];

            // Assert
            Assert.AreEqual(emoji, result);
        }
    }
}
