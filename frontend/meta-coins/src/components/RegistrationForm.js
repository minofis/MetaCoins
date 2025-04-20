import { useState } from 'react';
export default function RegistrationForm() {
 
  
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
  function printFormData(e){
    e.preventDefault();
    console.log(formData)
  }

   return (
        <div className="container-form">  
        <div className="content-form">
    
            
            <h1>Sign up</h1>
           
            <form onSubmit={printFormData} action="submit" className="contact-form">
    
            <label htmlFor="username">username:</label>
            <input id="username" name='username' onChange={saveFormData} value={formData.username} type="text" placeholder="username"/><br/>
    
    
            <label htmlFor="email">Email:</label>
            <input id="email" name='email' onChange={saveFormData} value={formData.email} type="email" placeholder="Email" autoComplete="email"/><br/>
            
            <label htmlFor="password">Password:</label>
            <input id="password" name='password'onChange={saveFormData}  value={formData.password} type="password" placeholder="password" autoComplete="password"/><br/>
             <br/>

            <button type="submit">Sign up</button> 
        </form>
        </div>
     
        </div>
     
      
    )

  }
  