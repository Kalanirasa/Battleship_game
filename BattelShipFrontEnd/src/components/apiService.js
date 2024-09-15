import axios from 'axios';

const API_BASE_URL = '/api/Battleship';

export const startNewGame = async () => {
  try {
    const response = await axios.post(`${API_BASE_URL}/start`);
    return response.data;
  } catch (error) {
    console.error('Error starting a new game:', error);
    throw error;
  }
};

export const makeTurn = async (requestBody) => {
  try {
    const response = await axios.post(`${API_BASE_URL}/turn`, requestBody);
    return response.data;
  } catch (error) {
    console.error('Error making a turn:', error);
    throw error;
  }
};
