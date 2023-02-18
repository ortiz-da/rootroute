/**
 * Represent a single node in the pathfinding grid.
 * Based on code and tutorial by Sebastian Lague (https://www.youtube.com/channel/UCmtyQOKKmrMVaKuRXz02jbQ).
 *   
 * Author: Ronen Ness.
 * Since: 2016. 
*/

namespace NesScripts.Controls.PathFind
{
    /// <summary>
    /// Represent a single node in the pathfinding grid.
    /// </summary>
    public class Node
    {
        // is this node walkable?
        public bool walkable;
        public int gridX;
        public int gridY;
        public float price;

        // calculated values while finding path
        public int gCost;
        public int hCost;
        public Node parent;

        /// <summary>
        /// Create the grid node.
        /// </summary>
        /// <param name="price">Price to walk on this node (set to 1.0f to ignore).</param>
        /// <param name="gridX">Node x index.</param>
        /// <param name="gridY">Node y index.</param>
        public Node(float price, int gridX, int gridY)
        {
            walkable = price != 0.0f;
            this.price = price;
            this.gridX = gridX;
            this.gridY = gridY;
        }

        /// <summary>
        /// Create the grid node.
        /// </summary>
        /// <param name="walkable">Is this tile walkable?</param>
        /// <param name="gridX">Node x index.</param>
        /// <param name="gridY">Node y index.</param>
        public Node(bool walkable, int gridX, int gridY)
        {
            this.walkable = walkable;
            price = walkable ? 1f : 0f;
            this.gridX = gridX;
            this.gridY = gridY;
        }

        /// <summary>
        /// Updates the grid node.
        /// </summary>
        /// <param name="price">Price to walk on this node (set to 1.0f to ignore).</param>
        /// <param name="gridX">Node x index.</param>
        /// <param name="gridY">Node y index.</param>
        public void Update(float price, int gridX, int gridY)
        {
            walkable = price != 0.0f;
            this.price = price;
            this.gridX = gridX;
            this.gridY = gridY;
        }

        /// <summary>
        /// Updates the grid node.
        /// </summary>
        /// <param name="walkable">Is this tile walkable?</param>
        /// <param name="gridX">Node x index.</param>
        /// <param name="gridY">Node y index.</param>
        public void Update(bool walkable, int gridX, int gridY)
        {
            this.walkable = walkable;
            price = walkable ? 1f : 0f;
            this.gridX = gridX;
            this.gridY = gridY;
        }

        /// <summary>
        /// Get fCost of this node.
        /// </summary>
        public int FCost
        {
            get { return gCost + hCost; }
        }
    }
}