import { useContext, useEffect, useRef, useState } from "react";
import { authContext } from "../../cores/context/auth";
import { io } from "socket.io-client";
import {
	createMessage,
	getConversationByUserId,
	getMessageByConversationId,
} from "../../services/chat.service";
import Conversation from "./conversation";
import MessageText from "./message-text";

const Messenger = () => {
	const {
		state: { user },
	} = useContext(authContext);
	const socket = useRef();
	const [currentChat, setCurrentChat] = useState(null);
	const [conversations, setConversations] = useState([]);
	const [messages, setMessages] = useState([]);
	const [newMessages, setNewMessages] = useState();
	const [arrivalMessage, setArrivalMessage] = useState(null);
	const scrollRef = useRef();

	useEffect(() => {
		socket.current = io("ws://localhost:8900");
		socket.current.emit("addUser", user.id);
		socket.current.on("getMessage", (data) => {
			setArrivalMessage({
				sender: data.senderId,
				text: data.text,
			});
		});
	}, [user]);

	useEffect(() => {
		if (arrivalMessage && arrivalMessage.senderId !== user.id) {
			setMessages((prev) => [...prev, arrivalMessage]);
		}
	}, [arrivalMessage, currentChat, user.id]);

	useEffect(() => {
		const getAllConversation = async () => {
			try {
				const response = await getConversationByUserId(user.id);
				setConversations(response.data.result);
			} catch (error) {
				console.log(error);
			}
		};
		getAllConversation();
	}, [user.id]);

	useEffect(() => {
		const getAllMessages = async () => {
			try {
				if (currentChat) {
					const response = await getMessageByConversationId(currentChat.id);
					setMessages(response.data.result);
				}
			} catch (error) {
				console.log(error);
			}
		};
		getAllMessages();
	}, [currentChat]);

	const handleSubmit = async (e) => {
		e.preventDefault();
		const message = {
			sender: user.id,
			conversationId: currentChat.id,
			text: newMessages,
		};
		let otherUserId =
			currentChat.senderId === user.id
				? currentChat.receiverId
				: currentChat.senderId;
		const receiverId = otherUserId;

		socket.current.emit("sendMessage", {
			senderId: user.id,
			receiverId: receiverId,
			text: newMessages,
		});

		try {
			const response = await createMessage(message);
			setMessages((prev) => [...prev, response.data.result]);
			setNewMessages("");
		} catch (error) {
			console.log(error);
		}
	};

	useEffect(() => {
		scrollRef?.current?.scrollIntoView({ behavior: "smooth" });
	}, [messages]);

	return (
		<div>
			<div className="fixed h-screen border-r-2 w-1/4 overflow-auto">
				<div className="flex p-5 border-b-2">
					<input
						className="py-2 px-2 border-2 border-gray-200 rounded-lg w-full"
						type="text"
						placeholder="Search Coversation"
					/>
				</div>
				{conversations.map((conversation) => (
					<div
						key={conversation.id}
						onClick={() => setCurrentChat(conversation)}
					>
						<Conversation conversation={conversation} />
					</div>
				))}
			</div>
			<div className="flex flex-row-reverse">
				<div className="w-3/4">
					<div className="h-[80vh] pb-5 w-full overflow-auto">
						{currentChat ? (
							<>
								{messages.map((message) => (
									<div ref={scrollRef}>
										<MessageText
											message={message}
											currentChat={currentChat}
											own={message.sender === user.id}
										/>
									</div>
								))}
								<div className="fixed w-3/4 bottom-0">
									<div className="flex">
										<textarea
											type="text"
											className="resize-none py-2 px-2 border-2 border-gray-300 bg-white w-full"
											value={newMessages}
											onChange={(e) => setNewMessages(e.target.value)}
											placeholder="Text..."
										/>
										<button
											className="px-10 bg-primary border-2 border-gray-300"
											onClick={handleSubmit}
										>
											<svg
												xmlns="http://www.w3.org/2000/svg"
												viewBox="0 0 24 24"
												fill="currentColor"
												className="w-6 h-6 text-white"
											>
												<path d="M3.478 2.405a.75.75 0 00-.926.94l2.432 7.905H13.5a.75.75 0 010 1.5H4.984l-2.432 7.905a.75.75 0 00.926.94 60.519 60.519 0 0018.445-8.986.75.75 0 000-1.218A60.517 60.517 0 003.478 2.405z" />
											</svg>
										</button>
									</div>
								</div>
							</>
						) : (
							<div className="flex justify-center items-center h-full">
								<span className="text-2xl text-gray-500">
									Open Conversation to see chat!
								</span>
							</div>
						)}
					</div>
				</div>
			</div>
		</div>
	);
};

export default Messenger;
