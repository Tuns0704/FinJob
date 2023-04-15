import axios from "axios";
import { config } from "./config";

export const getAllChats = (token) =>
	axios.get(`https://localhost:7252/conservations`, config(token));

export const getChat = (id, token) =>
	axios.get(`https://localhost:7252/conservations/${id}`, config(token));
