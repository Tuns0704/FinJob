import axios from "axios";
import { config } from "./config";

export const getAllChats = (token) =>
	axios.get(`${process.env.REACT_APP_API}/conservations`, config(token));

export const getChat = (id, token) =>
	axios.get(`${process.env.REACT_APP_API}/conservations/${id}`, config(token));
