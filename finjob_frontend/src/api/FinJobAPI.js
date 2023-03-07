import axios from "axios";

export default class FinJobAPI {
	constructor() {
		this.client = null;
		this.apiUrl = "";
	}

	init = () => {
		this.client = new axios.create({
			baseURL: this.apiUrl,
		});
		return this.client;
	};
}
