/**
 * Represent a grid of nodes we can search paths on.
 * Based on code and tutorial by Sebastian Lague (https://www.youtube.com/channel/UCmtyQOKKmrMVaKuRXz02jbQ).
 *   
 * Author: Ronen Ness.
 * Since: 2016. 
*/


//using UnityEditor.Experimental.GraphView;

namespace NesScripts.Controls.PathFind
{
    /// <summary>
    /// A 2D grid of nodes we use to find path.
    /// The grid mark which tiles are walkable and which are not.
    /// </summary>
    public class Grid
    {
        // nodes in grid
        public Node[,] nodes;

        // grid size
        int _gridSizeX, _gridSizeY;

        /// <summary>
        /// Create a new grid with tile prices.
        /// </summary>
        /// <param name="tilesCosts">A 2d array of tile prices.
        ///     0.0f = Unwalkable tile.
        ///     1.0f = Normal tile.
        ///     > 1.0f = costy tile.
        ///     < 1.0f = cheap tile.
        /// </param>
        public Grid(float[,] tilesCosts)
        {
            // create nodes
            CreateNodes(tilesCosts.GetLength(0), tilesCosts.GetLength(1));

            // init nodes
            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    nodes[x, y] = new Node(tilesCosts[x, y], x, y);
                }
            }
        }

        /// <summary>
        /// Create a new grid without tile prices, eg with just walkable / unwalkable tiles.
        /// </summary>
        /// <param name="walkableTiles">A 2d array representing which tiles are walkable and which are not.</param>
        public Grid(bool[,] walkableTiles)
        {
            // create nodes
            CreateNodes(walkableTiles.GetLength(0), walkableTiles.GetLength(1));

            // init nodes
            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    nodes[x, y] = new Node(walkableTiles[x, y] ? 1.0f : 0.0f, x, y);
                }
            }
        }

        /// <summary>
        /// Create the nodes grid and set size.
        /// </summary>
        /// <param name="width">Nodes grid width.</param>
        /// <param name="height">Nodes grid height.</param>
        private void CreateNodes(int width, int height)
        {
            _gridSizeX = width;
            _gridSizeY = height;
            nodes = new Node[_gridSizeX, _gridSizeY];
        }

        /// <summary>
        /// Updates the already created grid with new tile prices.
        /// </summary>
        /// <returns><c>true</c>, if grid was updated, <c>false</c> otherwise.</returns>
        /// <param name="tilesCosts">Tiles costs.</param>
        public void UpdateGrid(float[,] tilesCosts)
        {
            // check if need to re-create grid
            if (nodes == null ||
                _gridSizeX != tilesCosts.GetLength(0) ||
                _gridSizeY != tilesCosts.GetLength(1))
            {
                CreateNodes(tilesCosts.GetLength(0), tilesCosts.GetLength(1));
            }

            // update nodes
            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    nodes[x, y].Update(tilesCosts[x, y], x, y);
                }
            }
        }

        /// <summary>
        /// Updates the already created grid without new tile prices, eg with just walkable / unwalkable tiles.
        /// </summary>
        /// <returns><c>true</c>, if grid was updated, <c>false</c> otherwise.</returns>
        /// <param name="walkableTiles">Walkable tiles.</param>
        public void UpdateGrid(bool[,] walkableTiles)
        {
            // check if need to re-create grid
            if (nodes == null ||
                _gridSizeX != walkableTiles.GetLength(0) ||
                _gridSizeY != walkableTiles.GetLength(1))
            {
                CreateNodes(walkableTiles.GetLength(0), walkableTiles.GetLength(1));
            }

            // update grid
            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    nodes[x, y].Update(walkableTiles[x, y] ? 1.0f : 0.0f, x, y);
                }
            }
        }

        /// <summary>
        /// Get all the neighbors of a given tile in the grid.
        /// </summary>
        /// <param name="node">Node to get neighbors for.</param>
        /// <returns>List of node neighbors.</returns>
        public System.Collections.IEnumerable GetNeighbours(Node node, Pathfinding.DistanceType distanceType)
        {
            int x = 0, y = 0;
            switch (distanceType)
            {
                case Pathfinding.DistanceType.Manhattan:
                    y = 0;
                    for (x = -1; x <= 1; ++x)
                    {
                        var neighbor = AddNodeNeighbour(x, y, node);
                        if (neighbor != null)
                            yield return neighbor;
                    }

                    x = 0;
                    for (y = -1; y <= 1; ++y)
                    {
                        var neighbor = AddNodeNeighbour(x, y, node);
                        if (neighbor != null)
                            yield return neighbor;
                    }

                    break;

                case Pathfinding.DistanceType.Euclidean:
                    for (x = -1; x <= 1; x++)
                    {
                        for (y = -1; y <= 1; y++)
                        {
                            var neighbor = AddNodeNeighbour(x, y, node);
                            if (neighbor != null)
                                yield return neighbor;
                        }
                    }

                    break;
            }
        }

        /// <summary>
        /// Adds the node neighbour.
        /// </summary>
        /// <returns><c>true</c>, if node neighbour was added, <c>false</c> otherwise.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="node">Node.</param>
        /// <param name="neighbours">Neighbours.</param>
        Node AddNodeNeighbour(int x, int y, Node node)
        {
            if (x == 0 && y == 0)
            {
                return null;
            }

            int checkX = node.gridX + x;
            int checkY = node.gridY + y;

            if (checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY)
            {
                return nodes[checkX, checkY];
            }

            return null;
        }
    }
}