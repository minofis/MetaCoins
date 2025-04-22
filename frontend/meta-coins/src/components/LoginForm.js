export default function LoginForm() {

    return (
        <div className="container-form">  
        <div className="content-form">
    
            
            <h1>Log in</h1>
           
            <form  action="submit" className="contact-form">
    
            <label htmlFor="username">username:</label>
            <input id="username"   type="text" placeholder="username"/><br/>
            
            <label htmlFor="password">Password:</label>
            <input id="password"   type="password" placeholder="password" autoComplete="password"/><br/>
             <br/>
            <button type="submit">Log in</button> 
        </form>
        </div>
        </div>
     )      
}