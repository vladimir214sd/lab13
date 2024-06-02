using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace EmojiLibrary.Tests
{
    [TestClass]
    public class JournalEntryTests
    {
        [TestMethod]
        public void JournalEntry_Constructor_ShouldInitializeProperties()
        {
            // Arrange
            string collectionName = "EmojiCollection";
            string changeType = "Add";
            string itemData = "—и€ющий, <happy>";

            // Act
            var entry = new JournalEntry(collectionName, changeType, itemData);

            // Assert
            Assert.AreEqual(collectionName, entry.CollectionName);
            Assert.AreEqual(changeType, entry.ChangeType);
            Assert.AreEqual(itemData, entry.ItemData);
        }

        [TestMethod]
        public void JournalEntry_ToString_ShouldReturnCorrectFormat()
        {
            // Arrange
            string collectionName = "EmojiCollection";
            string changeType = "Add";
            string itemData = "—и€ющий, <happy>";
            var entry = new JournalEntry(collectionName, changeType, itemData);

            // Act
            string result = entry.ToString();

            // Assert
            string expected = "Collection: EmojiCollection, ChangeType: Add, Item: —и€ющий, <happy>";
            Assert.AreEqual(expected, result);
        }
    }

    [TestClass]
    public class JournalTests
    {
        [TestMethod]
        public void AddEntry_ShouldAddJournalEntryToList()
        {
            // Arrange
            var journal = new Journal();
            var entry = new JournalEntry("EmojiCollection", "Add", "—и€ющий, <happy>");

            // Act
            journal.AddEntry(entry);

            // Assert
            string result = journal.ToString();
            string expected = "Collection: EmojiCollection, ChangeType: Add, Item: —и€ющий, <happy>";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ToString_ShouldReturnAllJournalEntries()
        {
            // Arrange
            var journal = new Journal();
            var entry1 = new JournalEntry("EmojiCollection", "Add", "—и€ющий, <happy>");
            var entry2 = new JournalEntry("EmojiCollection", "Remove", "«адумчивый, <thinking>");

            journal.AddEntry(entry1);
            journal.AddEntry(entry2);

            // Act
            string result = journal.ToString();

            // Assert
            string expected = "Collection: EmojiCollection, ChangeType: Add, Item: —и€ющий, <happy>\n" +
                              "Collection: EmojiCollection, ChangeType: Remove, Item: «адумчивый, <thinking>";
            Assert.AreEqual(expected, result);
        }
    }
}
