import { useState } from 'react';
import { usersRegister } from '../services/usersService';
export default function RegistrationForm() {
const[error, setError] = useState("");

  const[formData, setFormData] = useState(
        {
          username: "",
          email: "",
          password: "",
        }
  
  );
  const saveFormData = (e) => {
    const {name,value} = e.target 
    setFormData(prev => ({ 
    ...prev,
    [name]: value
  })) 

  }

  function sendFormData(e){
    e.preventDefault();
   
    try {
     usersRegister(formData)
     setFormData({username: "",email: "",password: ""})
    } catch (error) {
      setError(error.response.data.message)
    }
  }

   return (
        <div className="container-form">  
        <div className="content-form">
    
            
            <h1>Sign up</h1>
           
            <form onSubmit={sendFormData} action="submit" className="contact-form">
    
            <label htmlFor="username">username:</label>
            <input id="username" name='username' onChange={saveFormData} value={formData.username} type="text" placeholder="username"/><br/>
    
    
            <label htmlFor="email">Email:</label>
            <input id="email" name='email' onChange={saveFormData} value={formData.email} type="email" placeholder="Email" autoComplete="email"/><br/>
            
            <label htmlFor="password">Password:</label>
            <input id="password" name='password'onChange={saveFormData}  value={formData.password} type="password" placeholder="password" autoComplete="password"/><br/>
             <br/>
            <div style={{ color: 'red' }}>
       
              {error}
              
            </div>
            <button type="submit">Sign up</button> 
        </form>
        </div>
     
        </div>
     
      
    )

  }
  