import { createContext, useEffect, useReducer } from "react";

export const authContext = createContext({
	state: {
		user: {},
		role: "",
		token: "",
		isAuthenticated: false,
	},
	dispatch: () => {},
});

const initialState = {
	user: {},
	role: "",
	token: "",
	isAuthenticated: false,
};

const LOGIN = "LOGIN";
const LOGOUT = "LOGOUT";

export const loginAction = (payload) => {
	return { type: LOGIN, payload };
};

export const logoutAction = () => {
	return { type: LOGOUT };
};

const authReducer = (state, action) => {
	switch (action.type) {
		case LOGIN:
			localStorage.setItem("authInfo", JSON.stringify(action.payload));
			return { ...state, isAuthenticated: true, ...action.payload };
		case LOGOUT:
			localStorage.removeItem("authInfo");
			return initialState;
		default:
			return state;
	}
};

const AuthContext = ({ children }) => {
	const [state, dispatch] = useReducer(authReducer, initialState);

	useEffect(() => {
		const authInfo = JSON.parse(localStorage.getItem("authInfo"));
		if (authInfo) {
			dispatch(loginAction(authInfo));
		}
	}, []);

	return (
		<authContext.Provider value={{ state, dispatch }}>
			{children}
		</authContext.Provider>
	);
};

export default AuthContext;
