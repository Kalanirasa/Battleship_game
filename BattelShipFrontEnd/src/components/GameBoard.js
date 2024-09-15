import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Grid from './Grid';
import ShipInfo from './ShipInfo';
import GameStatus from './GameStatus';

const GameBoard = () => {
  const [humanGrid, setHumanGrid] = useState([]);
  const [machineGrid, setMachineGrid] = useState([]);
  const [currentTurn, setCurrentTurn] = useState('Human');
  const [humanShips, setHumanShips] = useState([]);
  const [machineShips, setMachineShips] = useState([]);
  const [humanWaterPositions, setHumanWaterPositions] = useState([]);
  const [machineWaterPositions, setMachineWaterPositions] = useState([]);
  const [gameOver, setGameOver] = useState(false);
  const [winner, setWinner] = useState('');

  const createGrid = (ships, gridType, missPositions) => {
    const gridSize = 10;
    const grid = Array.from({ length: gridSize }, () => Array(gridSize).fill(gridType === 'Machine' ? '?' : '~'));
    ships.forEach((ship) => {
      ship.positions.forEach((pos) => {
        const row = pos.row.charCodeAt(0) - 'A'.charCodeAt(0);
        const col = pos.column - 1;
        grid[row][col] = pos.isHit ? 'X' : gridType === 'Machine' ? '?' : 'S';
      });
    });
    missPositions.forEach((pos) => {
      const row = pos.row.charCodeAt(0) - 'A'.charCodeAt(0);
      const col = pos.column - 1;
      grid[row][col] = pos.isMiss ? 'N' : gridType === 'Machine' ? '?' : '~';
    });
    return grid;
  };

  const startNewGame = async () => {
    try {
      const response = await axios.post('/api/Battleship/start');
      const { humanShips, machineShips } = response.data;
      setHumanShips(humanShips);
      setMachineShips(machineShips);
      setHumanWaterPositions([]);
      setMachineWaterPositions([]);
      setHumanGrid(createGrid(humanShips, 'Human', []));
      setMachineGrid(createGrid(machineShips, 'Machine', []));
      setCurrentTurn('Human');
      setGameOver(false);
      setWinner('');
    } catch (error) {
      console.error('Error starting a new game:', error);
    }
  };

  const getRandomPosition = () => {
    const gridSize = 10;
    const randomRow = Math.floor(Math.random() * gridSize);
    const randomCol = Math.floor(Math.random() * gridSize);
    return { rowIndex: randomRow, colIndex: randomCol };
  };

  const fireAtPosition = async (rowIndex, colIndex) => {
    if (currentTurn === 'Machine') {
      const { rowIndex: randomRow, colIndex: randomCol } = getRandomPosition();
      rowIndex = randomRow;
      colIndex = randomCol;
    }
    try {
      const row = String.fromCharCode('A'.charCodeAt(0) + rowIndex);
      const column = colIndex + 1;
      const requestBody = {
        row,
        column,
        isHuman: currentTurn === 'Human',
        humanShips,
        machineShips,
        humanWaterPositions,
        machineWaterPositions,
      };
      const response = await axios.post('/api/Battleship/turn', requestBody);
      const { updatedHumanShips, updatedMachineShips, updatedMissMachinePositions, updatedMissHumanPositions, isGameOver } = response.data;

      setHumanShips(updatedHumanShips);
      setMachineShips(updatedMachineShips);
      setHumanWaterPositions(updatedMissHumanPositions);
      setMachineWaterPositions(updatedMissMachinePositions);
      setHumanGrid(createGrid(updatedHumanShips, 'Human', updatedMissHumanPositions));
      setMachineGrid(createGrid(updatedMachineShips, 'Machine', updatedMissMachinePositions));

      if (isGameOver) {
        setWinner(currentTurn);
        setGameOver(true);
      } else {
        setCurrentTurn(currentTurn === 'Human' ? 'Machine' : 'Human');
      }
    } catch (error) {
      console.error('Error firing at position:', error.response?.data || error.message);
    }
  };

  useEffect(() => {
    if (currentTurn === 'Machine' && !gameOver) {
      const { rowIndex, colIndex } = getRandomPosition();
      fireAtPosition(rowIndex, colIndex);
    }
  }, [currentTurn]);

  return (
    <div>
      <GameStatus
        gameOver={gameOver}
        winner={winner}
        onStartNewGame={startNewGame}
        currentTurn={currentTurn} // Pass the currentTurn state here
      />
      {!gameOver && (
        <div className="game-board">
          <div>
            <h3>Human's Grid</h3>
            <Grid grid={humanGrid} onCellClick={fireAtPosition} />
          </div>
          <ShipInfo ships={humanShips} title="Human Ships" />
          <div>
            <h3>Machine's Grid</h3>
            <Grid grid={machineGrid} onCellClick={fireAtPosition} />
          </div>
          <ShipInfo ships={machineShips} title="Machine Ships" />
        </div>
      )}
    </div>
  );
};

export default GameBoard;