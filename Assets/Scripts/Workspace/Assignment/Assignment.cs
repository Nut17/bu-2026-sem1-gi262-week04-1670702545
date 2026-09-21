using UnityEngine;
using System.Collections.Generic;

using System.Linq;
using System.Text;
namespace Assignment
{
    public class Assignment : MonoBehaviour
    {
        public void Start()
        {
            // AS01_CountWords();
            // AS02_CountNumber();
            // AS03_CheckValidBrackets();
            // AS04_PrintReverseLinkedList();
            // AS05_FindMiddleElement();
            // AS06_MergeDictionaries();
            // AS07_RemoveDuplicatesFromLinkedList();
            // AS08_TopFrequentNumber();
            // AS09_PlayerInventory();
            // AS10_GameEventQueue();
            AS11_PlayerStatsTracker();
        }

        #region Assignment

        [Header("AS01 - Count Words")]
        [SerializeField] private string[] as01Words;

        // Count how many times each word appears in the array (case-insensitive) and log the results
        public void AS01_CountWords()
        {
            string[] words = as01Words;

            if (words == null || words.Length == 0)
            {
                Debug.Log("AS01: No words to process");
                return;
            }

            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            foreach (string word in words)
            {
                if (string.IsNullOrEmpty(word)) continue;

                string key = word.ToLowerInvariant();

                if (wordCount.ContainsKey(key))
                    wordCount[key]++;
                else
                    wordCount[key] = 1;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"AS01: Total words = {words.Length}, Unique words = {wordCount.Count}");
            foreach (var kvp in wordCount)
            {
                sb.AppendLine($"  \"{kvp.Key}\" -> {kvp.Value} time(s)");
            }

            Debug.Log(sb.ToString());
        }

        [Header("AS02 - Count Number")]
        [SerializeField] private int[] as02Numbers;

        // Count how many times each number appears in the array and log the results
        public void AS02_CountNumber()
        {
            int[] numbers = as02Numbers;

            if (numbers == null || numbers.Length == 0)
            {
                Debug.Log("AS02: No numbers to process");
                return;
            }

            Dictionary<int, int> numberCount = new Dictionary<int, int>();

            foreach (int number in numbers)
            {
                if (numberCount.ContainsKey(number))
                    numberCount[number]++;
                else
                    numberCount[number] = 1;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"AS02: Total numbers = {numbers.Length}, Unique values = {numberCount.Count}");
            foreach (var kvp in numberCount)
            {
                sb.AppendLine($"  {kvp.Key} -> {kvp.Value} time(s)");
            }

            Debug.Log(sb.ToString());
        }

        [Header("AS03 - Check Valid Brackets")]
        [SerializeField] private string as03Input;

        // Check whether the brackets (), {}, [] in the string are valid, using a Stack
        public void AS03_CheckValidBrackets()
        {
            string input = as03Input;

            bool isValid = IsValidBrackets(input);

            Debug.Log($"AS03: \"{input}\" -> {(isValid ? "Valid" : "Invalid")}");
        }

        private bool IsValidBrackets(string input)
        {
            if (string.IsNullOrEmpty(input)) return true;

            Stack<char> stack = new Stack<char>();
            Dictionary<char, char> pairs = new Dictionary<char, char>
            {
                { ')', '(' },
                { '}', '{' },
                { ']', '[' }
            };

            foreach (char c in input)
            {
                if (c == '(' || c == '{' || c == '[')
                {
                    stack.Push(c);
                }
                else if (c == ')' || c == '}' || c == ']')
                {
                    if (stack.Count == 0 || stack.Pop() != pairs[c])
                        return false;
                }
            }

            return stack.Count == 0;
        }

        [Header("AS04 - Print Reverse Linked List")]
        [SerializeField] private IntLinkedListInput as04List = new IntLinkedListInput();

        // Print the values of the Linked List from tail to head
        public void AS04_PrintReverseLinkedList()
        {
            LinkedList<int> list = as04List.GetLinkedList();

            if (list == null || list.Count == 0)
            {
                Debug.Log("AS04: List is empty");
                return;
            }

            StringBuilder sb = new StringBuilder("AS04 (reverse): ");
            LinkedListNode<int> node = list.Last;
            while (node != null)
            {
                sb.Append(node.Value);
                if (node.Previous != null) sb.Append(" -> ");
                node = node.Previous;
            }

            Debug.Log(sb.ToString());
        }

        [Header("AS05 - Find Middle Element")]
        [SerializeField] private StringLinkedListInput as05List = new StringLinkedListInput();

        // Find the middle element of the Linked List using slow/fast pointers
        public void AS05_FindMiddleElement()
        {
            LinkedList<string> list = as05List.GetLinkedList();

            if (list == null || list.Count == 0)
            {
                Debug.Log("AS05: List is empty");
                return;
            }

            LinkedListNode<string> slow = list.First;
            LinkedListNode<string> fast = list.First;

            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }

            Debug.Log($"AS05: The middle element is \"{slow.Value}\"");
        }

        [Header("AS06 - Merge Dictionaries")]
        [SerializeField] private StringIntDictionaryInput as06FirstDictionary = new StringIntDictionaryInput();
        [SerializeField] private StringIntDictionaryInput as06SecondDictionary = new StringIntDictionaryInput();

        // Merge two dictionaries together; if a key exists in both, sum their values
        public void AS06_MergeDictionaries()
        {
            Dictionary<string, int> dict1 = as06FirstDictionary.GetDictionary();
            Dictionary<string, int> dict2 = as06SecondDictionary.GetDictionary();

            Dictionary<string, int> merged = new Dictionary<string, int>(dict1 ?? new Dictionary<string, int>());

            if (dict2 != null)
            {
                foreach (var kvp in dict2)
                {
                    if (merged.ContainsKey(kvp.Key))
                        merged[kvp.Key] += kvp.Value;
                    else
                        merged[kvp.Key] = kvp.Value;
                }
            }

            StringBuilder sb = new StringBuilder("AS06: Merged Dictionary\n");
            foreach (var kvp in merged)
            {
                sb.AppendLine($"  {kvp.Key} -> {kvp.Value}");
            }

            Debug.Log(sb.ToString());
        }

        [Header("AS07 - Remove Duplicates From Linked List")]
        [SerializeField] private IntLinkedListInput as07List = new IntLinkedListInput();

        // Remove duplicate elements from the Linked List while preserving the original order
        public void AS07_RemoveDuplicatesFromLinkedList()
        {
            LinkedList<int> list = as07List.GetLinkedList();

            if (list == null || list.Count == 0)
            {
                Debug.Log("AS07: List is empty");
                return;
            }

            HashSet<int> seen = new HashSet<int>();
            LinkedListNode<int> node = list.First;

            while (node != null)
            {
                LinkedListNode<int> next = node.Next;

                if (seen.Contains(node.Value))
                {
                    list.Remove(node);
                }
                else
                {
                    seen.Add(node.Value);
                }

                node = next;
            }

            StringBuilder sb = new StringBuilder("AS07 (after removing duplicates): ");
            sb.Append(string.Join(" -> ", list));
            Debug.Log(sb.ToString());
        }

        [Header("AS08 - Top Frequent Number")]
        [SerializeField] private int[] as08Numbers;

        // Find the number that appears most frequently in the array
        public void AS08_TopFrequentNumber()
        {
            int[] numbers = as08Numbers;

            if (numbers == null || numbers.Length == 0)
            {
                Debug.Log("AS08: No numbers to process");
                return;
            }

            Dictionary<int, int> counts = new Dictionary<int, int>();
            foreach (int n in numbers)
            {
                if (counts.ContainsKey(n))
                    counts[n]++;
                else
                    counts[n] = 1;
            }

            int topNumber = 0;
            int topCount = -1;
            foreach (var kvp in counts)
            {
                if (kvp.Value > topCount)
                {
                    topCount = kvp.Value;
                    topNumber = kvp.Key;
                }
            }

            Debug.Log($"AS08: The most frequent number is {topNumber} (appears {topCount} time(s))");
        }

        [Header("AS09 - Player Inventory")]
        [SerializeField] private StringIntDictionaryInput as09Inventory = new StringIntDictionaryInput();
        [SerializeField] private string as09ItemName;
        [SerializeField] private int as09Quantity;

        // Add/subtract an item's quantity in the inventory; remove the item if its quantity drops to <= 0
        public void AS09_PlayerInventory()
        {
            Dictionary<string, int> inventory = as09Inventory.GetDictionary();
            string itemName = as09ItemName;
            int quantity = as09Quantity;

            if (inventory == null)
            {
                Debug.Log("AS09: Invalid inventory");
                return;
            }

            if (string.IsNullOrEmpty(itemName))
            {
                Debug.Log("AS09: No item name specified");
                return;
            }

            if (inventory.ContainsKey(itemName))
            {
                inventory[itemName] += quantity;
                if (inventory[itemName] <= 0)
                {
                    inventory.Remove(itemName);
                    Debug.Log($"AS09: Item \"{itemName}\" ran out and was removed from the inventory");
                }
                else
                {
                    Debug.Log($"AS09: Updated item \"{itemName}\" to quantity {inventory[itemName]}");
                }
            }
            else
            {
                if (quantity > 0)
                {
                    inventory[itemName] = quantity;
                    Debug.Log($"AS09: Added new item \"{itemName}\" with quantity {quantity}");
                }
                else
                {
                    Debug.Log($"AS09: Item \"{itemName}\" does not exist in the inventory and the given quantity is invalid");
                }
            }
        }

        [Header("AS10 - Game Event Queue")]
        [SerializeField] private GameEventLinkedListInput as10EventQueue = new GameEventLinkedListInput();

        // Process events one by one in queue order (FIFO) until the queue is empty
        // Note: this code assumes GameEvent works with its default ToString(); if your actual
        // GameEvent class has specific fields you want shown (e.g. EventName, Priority), let me know
        // and I'll adjust this to reference them directly.
        public void AS10_GameEventQueue()
        {
            LinkedList<GameEvent> eventQueue = as10EventQueue.GetLinkedList();

            if (eventQueue == null || eventQueue.Count == 0)
            {
                Debug.Log("AS10: Event queue is empty");
                return;
            }

            StringBuilder sb = new StringBuilder("AS10: Processing Event Queue\n");

            while (eventQueue.Count > 0)
            {
                GameEvent currentEvent = eventQueue.First.Value;
                eventQueue.RemoveFirst();

                sb.AppendLine($"  Processing: {currentEvent}");
            }

            Debug.Log(sb.ToString());
        }

        [Header("AS11 - Player Stats Tracker")]
        [SerializeField] private StringIntDictionaryInput as11PlayerStats = new StringIntDictionaryInput();
        [SerializeField] private string as11StatName;
        [SerializeField] private int as11Value;

        // Update a player's stat value; if the stat already exists, add to it, otherwise create it
        public void AS11_PlayerStatsTracker()
        {
            Dictionary<string, int> playerStats = as11PlayerStats.GetDictionary();
            string statName = as11StatName;
            int value = as11Value;

            if (playerStats == null)
            {
                Debug.Log("AS11: Invalid player stats");
                return;
            }

            if (string.IsNullOrEmpty(statName))
            {
                Debug.Log("AS11: No stat name specified");
                return;
            }

            if (playerStats.ContainsKey(statName))
            {
                playerStats[statName] += value;
                Debug.Log($"AS11: Updated stat \"{statName}\" to {playerStats[statName]}");
            }
            else
            {
                playerStats[statName] = value;
                Debug.Log($"AS11: Created new stat \"{statName}\" = {value}");
            }
        }

        #endregion
    }
}
