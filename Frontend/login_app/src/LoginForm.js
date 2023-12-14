import React, { useRef } from "react";
import './LoginForm.css';

const LoginForm = (props) => {
	const nameRef =  useRef();
	const passwordRef =  useRef();
	const handleSubmit = (event) =>{
		event.preventDefault();
		props.onSubmit({
			login: nameRef.current.value,
			password: passwordRef.current.value,
		});
	}

	return (
		<form className="form">
			<h1>Login</h1>
			<label htmlFor="name">Name</label>
			<input type="text" id="name" ref={nameRef}/>
			<label htmlFor="password">Password</label>
			<input type="password" id="password" ref={passwordRef}/>
			<button type="submit" onClick={handleSubmit}>Continue</button>
		</form>
	)
}

export default LoginForm;