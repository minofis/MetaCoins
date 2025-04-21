import axios from 'axios';
export const usersRegister  = async (data) => {
     try{
    const response = await axios.post ('http://localhost:5001/meta-coins/users/register',data)
    return response.data
        }  
         
    catch(error){
       return error
    }
   
    

    
} 