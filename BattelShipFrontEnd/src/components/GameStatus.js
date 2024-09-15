import React from 'react';

const GameStatus = ({ gameOver, winner, onStartNewGame, currentTurn }) => {
  return (
    <div className="game-status">
      <h2>Status</h2>
      <p>Current Turn: {currentTurn}</p>
      {gameOver ? (
        <div>
          <h3>Game Over!</h3>
          <p>Winner: {winner}</p>
          <button onClick={onStartNewGame}>Start New Game</button>
        </div>
      ) : (
        <button onClick={onStartNewGame}>Start New Game</button>
      )}
    </div>
  );
};

export default GameStatus;