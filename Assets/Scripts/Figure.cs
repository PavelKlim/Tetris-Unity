using UnityEngine;
using System.Collections.Generic;


public enum Figures { Steps, Square, Stick, Pedestal, Horse };

public class Figure //: MonoBehaviour 
{
	public Figures nameFigure;
	public bool[,] form;
	public Point position;
	
	public Figure(Model model)
	{
		int i = Random.Range(0,5);
		switch (i)
		{
		case 0:
			this.form = new bool[2, 3] { { true, true, true }, { true, false, false } };
			this.nameFigure = Figures.Horse;
			if (Random.Range(0.0f, 1.0f) > 0.5f)
				this.Reflector();
			break;
		case 1:
			this.form = new bool[2, 3] { { false, true, false }, { true, true, true } };
			this.nameFigure = Figures.Pedestal;
			break;
		case 2:
			this.form = new bool[2, 2] { { true, true }, { true, true } };
			this.nameFigure = Figures.Square;
			break;
		case 3:
			this.form = new bool[2, 3] { { true, true, false }, { false, true, true } };
			this.nameFigure = Figures.Steps;
			if (Random.Range(0.0f, 1.0f) > 0.5f)
				this.Reflector();
			break;
		case 4:
			this.form = new bool[1, 4] { { true, true, true, true } };
			this.nameFigure = Figures.Stick;
			break;
		}
		this.position = new Point((int)(model.cells.GetLength(1) / 2) - (this.form.GetLength(1) / 2), -2*(this.form.GetLength(0)));
		
	}

	public Figure(Figure figureCopy)
	{
		if (figureCopy != null && this != null)
		{
			this.nameFigure = figureCopy.nameFigure;
			this.position = figureCopy.position;
			this.form = new bool[figureCopy.form.GetLength(0), figureCopy.form.GetLength(1)];
			for (int y = 0; y < this.form.GetLength(0); y++)
			{
				for (int x = 0; x < this.form.GetLength(1); x++)
				{
					this.form[y, x] = figureCopy.form[y, x];
				}
			}
		}
	}
	
	
	public bool isFill(int x, int y)
	{
		bool isCellFilled = (x >= this.position.X && x < (this.position.X + this.form.GetLength(1))) && (y >= this.position.Y && y < (this.position.Y + this.form.GetLength(0)));
		if (isCellFilled)
		{
			int _x = x - this.position.X;
			int _y = y - this.position.Y;
			if (this.form[_y, _x])
				return true;
		}
		return false;
	}
	//-----------------------------------------------------------------------------------------------
	
	public bool rotateRight(Model model)
	{
		Figure figureTest = new Figure(this); 
		bool[,] rotatedFigure = new bool[this.form.GetLength(1), this.form.GetLength(0)];
		for (int x = 0; x < rotatedFigure.GetLength(1); x++)
		{
			for (int y = 0; y < rotatedFigure.GetLength(0); y++)
			{
				rotatedFigure[y, x] = this.form[this.form.GetLength(0)-1-x, y];
			}
		}
		figureTest.form = rotatedFigure;
		if (figureTest.isInGameArea(model) && !figureTest.isOverlaying(model) && !this.isCollisionBottom(model))
		{
			this.form = figureTest.form;
			return true;
		}
		return false;
	}
	//-----------------------------------------------------------------------------------------------
	
	public bool rotateLeft(Model model)
	{
		Figure figureTest = new Figure(this); 
		bool[,] rotatedFigure = new bool[this.form.GetLength(1), this.form.GetLength(0)];
		for (int x = 0; x < rotatedFigure.GetLength(1); x++)
		{
			for (int y = 0; y < rotatedFigure.GetLength(0); y++)
			{
				rotatedFigure[y, x] = this.form[x, this.form.GetLength(1) - 1 - y];
			}
		}
		figureTest.form = rotatedFigure;
		if (figureTest.isInGameArea(model) && !figureTest.isOverlaying(model) && !this.isCollisionBottom(model))
		{
			this.form = figureTest.form;
			return true;
		}
		return false;
	}
	//-----------------------------------------------------------------------------------------------
	
	public bool isCollisionBottom(Model model)
	{
		if (!(this.position.Y + this.form.GetLength(0) <= model.cells.GetLength(0)-1))
			return true;
		else
			for (int y = this.form.GetLength(0) - 1; y >= 0; y--)
		{
			for (int x = 0; x < this.form.GetLength(1); x++)
			{
				if (this.position.Y >= 0)
					if (this.form[y, x] && model.isFill(this.position.X + x, this.position.Y+y+1))
						return true;
			}
		}
		return false;
	}
	//-----------------------------------------------------------------------------------------------
	
	public bool isCollisionLeft(Model model)
	{
		if (!(this.position.X - 1 >= 0))
			return true;
		else
			for (int x = 0; x < this.form.GetLength(1); x++)
		{
			for (int y = 0; y < this.form.GetLength(0); y++)
			{
				if (this.form[y, x] && model.isFill(this.position.X + x - 1, this.position.Y + y))
					return true;
			}
		}
		return false;
	}
	//-----------------------------------------------------------------------------------------------
	
	public bool isCollisionRight(Model model)
	{
		if (!(this.position.X + this.form.GetLength(1) < model.cells.GetLength(1)))
			return true;
		else
			for (int x = this.form.GetLength(1) - 1; x >= 0; x--)
		{
			for (int y = 0; y < this.form.GetLength(0); y++)
			{
				if (this.form[y, x] && (model.isFill(this.position.X + x + 1, this.position.Y + y)))
					return true;
			}
		}
		return false;
	}
	//-----------------------------------------------------------------------------------------------
	
	public bool MoveDown(Model model)
	{
		if (!this.isCollisionBottom(model))
		{
			this.position.Y += 1;
			return true;
		}
			return false;
	}
	//-----------------------------------------------------------------------------------------------
	
	public bool MoveLeft(Model model)
	{
		if (!this.isCollisionLeft(model))
		{
			this.position.X -= 1;
			return true;
		}
		return false;
	}
	//-----------------------------------------------------------------------------------------------
	
	public bool MoveRight(Model model)
	{
		if (!this.isCollisionRight(model))
		{
			this.position.X += 1;
			return true;
		}
		return false;
	}
	//-----------------------------------------------------------------------------------------------
	
	public bool isOverlaying(Model model)
	{
		if (this.position.Y >= 0)
		{
			for (int x = 0; x < this.form.GetLength(1); x++)
			{
				for (int y = 0; y < this.form.GetLength(0); y++)
				{
					if (model.cells[y + this.position.Y, x + this.position.X])
					{
						return true;
					}
				}
			}
		}
		return false;
	}
	//-----------------------------------------------------------------------------------------------
	
	public bool isInGameArea(Model model)
	{
		bool isInGameArea = ((this.position.X + this.form.GetLength(1) - 1) <= model.cells.GetLength(1) - 1) && ((this.position.Y + this.form.GetLength(0) - 1) <= model.cells.GetLength(0) - 1);
		if (isInGameArea)
			return true;
		return false;
	}
	//-----------------------------------------------------------------------------------------------
	
	public void Reflector()
	{
		if (this != null && this.form != null)
		{
			Figure figureTemp = new Figure(this);
			for (int y = 0; y < this.form.GetLength(0); y++)
			{
				for (int x = 0; x < this.form.GetLength(1); x++)
				{
					this.form[y, x] = figureTemp.form[y, figureTemp.form.GetLength(1) - 1 - x];
				}
			}
		}
	}
}
