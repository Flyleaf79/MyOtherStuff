#region MyRegion
using UnityEngine;
using System.Collections;
using System; // to be able to use Icomparable
/*
//SORTING BY HEAP can be used by other classes not just NODES.
public class Heap<T> where T : IHeapItem<T>
{
    T[] items; // items in the Heap.
    int currentItemCount; // to keep track of the heap item counts

    // constructor
    // we need to know what the size of the array is
    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    // to add heap items
    public void Add(T item)
    {
        // We will like for every item to keep track of their own index in the heap
        // We'll also need each item to be able to compare to each other item in the heap

        item.HeapIndex = currentItemCount;
        // item is set to the end of the array in Items
        // This isnt neccesssarly where it belongs
        items[currentItemCount] = item;

        SortUp(item);
        currentItemCount++;

    }

    public T RemoveFirst()
    {
        // WHEN adding a new item to the heap and being able to organize it. we want to be able to minimize the amount of processing power
        // by adding the new item to the top of the tree(first element in the array) than we can sort down in order to find the right fit.
        // This will make it so we take less steps to finding its proper position in the heap.

        T firstItem = items[0]; // Saving the first item
        currentItemCount--;
        items[0] = items[currentItemCount]; //set the first item in the heap to the last item in the heap
        items[0].HeapIndex = 0; // store its new index
        SortDown(items[0]);
        return firstItem;
    }

    // This is incase we ever want to change the priority of an item as we do in our path finding
    // where we might find a node in the open set which we want to update with a lower F cost because we found a new path to it
    // we need to be able to update its position in the HEAP 
    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    public int Count
    {
        get
        {
            return currentItemCount;
        }
        // no set;
    }

    public bool Contains(T item)
    {
        //to check if the heap contains the specific item
        return Equals(items[item.HeapIndex], item);
    }

    // decrease priority
    void SortDown(T item)
    {
        // sorts down the heap. 
        // parent = n - 1 / 2;
        // childA = 2n + 1;
        // childB = 2n + 2;
        while (true)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;

            // item is the parent. than you multiply it by 2 and add 1. there is nothing after than then the item has no children.
            if (childIndexLeft < currentItemCount)
            {
                //store by default. Stores the childindexleft  as (the highest value)
                swapIndex = childIndexLeft;
                // checks if item has a childitem to the right.
                if (childIndexRight < currentItemCount)
                {
                    //checks if the childleft has a lower prioity than the childright
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                    {
                        //if childleft has a lower priority than set SwapIndex to the CHILDRIGHT 
                        swapIndex = childIndexRight;
                    }
                }
                // the above process is all about getting the highest priority child
                // NOW CHECK if the item (parent) is lower priority than its highest prioity child
                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    #region Reminder
                    /*

                    /////REMINDER//// 
                    remember tha this method is for sorting downwards. NEW items are being put to sort down the heap tree
                    

                    
                    #endregion

                    //if parent is lower than the child than swap index.
                    Swap(item, items[swapIndex]);
                }
                else
                {
                    // if parent has a higher prioity than allm of its children than its in the right place and we can exit out of the loop
                    return;
                }
            }
            else
            {
                // if Parent doesnt have an children than you can exit out of the loop
                return;
            }
        }
    }

    void SortUp(T item)
    {
        // parent = n - 1 / 2;
        // childA = 2n + 1;
        // childB = 2n + 2;

        int parentIndex = (item.HeapIndex - 1) / 2;

        while (true)
        {
            T parentItem = items[parentIndex];
            // How this is implemented is: if the Item has a higher priority than its parent
            // than it will return 1, if it has the same priority it'll return 0
            // if it has less priority itll return -1
            // in oue case, if it has a lower fCost than we want to Swap it with its parent.
            if (item.CompareTo(parentItem) > 0)
            {

                Swap(item, parentItem);
            }
            else
            {
                //if it is no longer of higher piority of its parent index than break;
                break;
            }

            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        // We also want to swap the heap index values
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}
*/

   /*
public interface IHeapItem<T> : IComparable<T>
{
    // So that each heap item holds its own index number
    // als sets;
    int HeapIndex
    {
        get;
        set;
    }
} 
*/
#endregion