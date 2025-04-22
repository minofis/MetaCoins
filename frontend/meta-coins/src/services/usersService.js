import axios from 'axios';
export const usersRegister  = async (data) => {
  const response = await axios.post ('http://localhost:5001/meta-coins/users/register',data)
  return response.data 
} 