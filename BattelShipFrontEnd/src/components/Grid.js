import React from 'react';

const Grid = ({ grid, onCellClick }) => (
  <div className="grid">
    {grid.map((row, rowIndex) => (
      <div key={rowIndex} className="grid-row">
        {row.map((cell, colIndex) => (
          <button
            key={`${rowIndex}-${colIndex}`}
            onClick={() => onCellClick(rowIndex, colIndex)}
            className="grid-cell"
          >
            {cell}
          </button>
        ))}
      </div>
    ))}
  </div>
);

export default Grid;
