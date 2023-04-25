import axios from "axios";
import { config } from "./config";

export const createConversation = (body, token) =>
	axios.post(`${process.env.REACT_APP_API}/Conversation`, body, config(token));

export const getConversationByUserId = (userId, token) =>
	axios.get(
		`${process.env.REACT_APP_API}/Conversation/${userId}`,
		config(token)
	);

export const createMessage = (body, token) =>
	axios.post(`${process.env.REACT_APP_API}/Message`, body, config(token));

export const getMessageByConversationId = (id, token) =>
	axios.get(`${process.env.REACT_APP_API}/Message/${id}`, config(token));
