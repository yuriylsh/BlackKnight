import React, { useMemo, useState } from "react";
import "./LoginAttemptList.css";

const LoginAttempt = (props) => <li {...props}>{props.children}</li>;

const matchesFilter = (filter, input) => input.search(new RegExp(filter, "i")) > -1;

const LoginAttemptList = ({attempts}) => {
	const [filter, setFilter] = useState('');
	const filteredAttempts = useMemo(
		() => {
			if(!filter) return attempts;
			return attempts.filter(({login, password}) => matchesFilter(filter, login) || matchesFilter(filter, password));
		}
		, [attempts, filter]);

	return (
		<div className="Attempt-List-Main">
			 <p>Recent activity</p>
			  <input type="input" placeholder="Filter..." onChange={(event) => setFilter(event.target.value)} />
			<ul className="Attempt-List">
				{filteredAttempts.map((attempt, i) => <LoginAttempt key={i}><LoginEntry {...attempt}/></LoginAttempt>)}			
			</ul>
		</div>
	);
};

const LoginEntry = ({login, password}) => (
	<>
		<span className="output">{login}</span>,&nbsp;<span className="output">{password}</span>
	</>);

export default LoginAttemptList;