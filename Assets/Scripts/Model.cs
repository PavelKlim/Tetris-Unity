using UnityEngine;
using System.Collections;

public class Model //: MonoBehaviour 
{
	public bool[,] cells;
	public Model(int width, int height)
	{
		this.cells = new bool[height, width];
		for (int i = 0; i < this.cells.GetLength(0); i++)
			for (int y = 0; y < this.cells.GetLength(1); y++)
				this.cells[i, y] = false;
	}
	
	
	//-----------------------------------------------------------------------------------------------
	
	public bool isFill(int x, int y)
	{
		if (x >= 0 && y >= 0)
			return this.cells[y, x];
		return false;
	}
	
	//-----------------------------------------------------------------------------------------------
	
	public bool isEndOfGame()
	{
		for (int x = 0; x < this.cells.GetLength(1); x++)
			if (this.cells[0, x])
				return true;
		return false;
	}
	//-----------------------------------------------------------------------------------------------
	
	public void Add(Figure figure)
	{
		for (int y = 0; y < figure.form.GetLength(0); y++)
			for (int x = 0; x < figure.form.GetLength(1); x++)
				if (figure.form[y, x])
					this.cells[y + figure.position.Y, x + figure.position.X] = true;
	}
	//-----------------------------------------------------------------------------------------------
	
	public bool fullLinesClear(CubeInfo [,]cubesArray, ref ArrayList height)
	{
		bool isFullLine = false;
		int counter;

		for (int i = 0; i < this.cells.GetLength(0); i++)
		{
			counter = 0;
			for (int y = 0; y < this.cells.GetLength(1); y++)
			{
				if (this.cells[i, y]) counter++;
			}
			if (counter == this.cells.GetLength(1))
			{
				isFullLine = true;
				if(height == null) height=new ArrayList();
				height.Add (i);
				for (int g = i; g > 0; g--)
				{
					for (int h = 0; h < this.cells.GetLength(1); h++)
					{
						if ((g - 1) <= (this.cells.GetLength(0) - 1))
						{
							this.cells[g, h] = this.cells[g - 1, h];
							cubesArray[g, h].cubeColored.renderer.material.color = cubesArray[g-1, h].cubeColored.renderer.material.color;
						}
						else
							this.cells[g, h] = false;
					}
					
				}
			}
		}
		return isFullLine;
	}	
}
