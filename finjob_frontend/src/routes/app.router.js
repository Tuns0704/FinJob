import { useContext } from "react";
import { Routes, Route } from "react-router-dom";
import LoginPage from "../features/login-page/login";
import RegisterPage from "../features/register-page/register";
import { authContext } from "../cores/context/auth";
import AdminContainer from "../containers/admin.container";
import AppContainer from "../containers/app.container";
import LandingPage from "../features/landing-page/landing-page";
import HomePage from "../features/home-page/home-page";
import NotFound from "../features/error/not-found";
import AdminManageJob from "../features/admin-manage-job";
import JobDetail from "../features/detail";
import Messenger from "../features/connection";
import AdminManageUser from "../features/admin-manage-user";
import UserProfile from "../features/user-profile";

const AppRouter = () => {
	const {
		state: { isAuthenticated, role },
	} = useContext(authContext);

	return (
		<Routes>
			<Route
				path="/"
				element={role === "Admin" ? <AdminContainer /> : <AppContainer />}
			>
				{!isAuthenticated && (
					<>
						<Route path="" element={<LandingPage />} />
						<Route path="login" element={<LoginPage />} />
						<Route path="register" element={<RegisterPage />} />
					</>
				)}
				{isAuthenticated &&
					(role === "Admin" ? (
						<>
							<Route path="app" element={<AdminContainer />} />
							<Route path="/" element={<AdminManageUser />} />
							<Route path="/jobs" element={<AdminManageJob />} />
							<Route path="/detail/:id" element={<JobDetail />} />
							<Route path="/profile/:userId" element={<UserProfile />} />
						</>
					) : role === "BusinessEmployer" ? (
						<>
							<Route path="app" element={<AppContainer />} />
							<Route path="/" element={<HomePage />} />
							<Route path="/detail/:id" element={<JobDetail />} />
							<Route path="/messages" element={<Messenger />} />
							<Route path="profile/:id" element={<UserProfile />} />
							{/*<Route path='detail/:id' element={<PostDetail />} />  */}
						</>
					) : (
						<>
							<Route path="app" element={<AppContainer />} />
							<Route path="/" element={<HomePage />} />
							<Route path="/detail/:id" element={<JobDetail />} />
							<Route path="profile/:id" element={<UserProfile />} />
							<Route path="/messages" element={<Messenger />} />
						</>
					))}
				<Route path="*" element={<NotFound />} />
			</Route>
		</Routes>
	);
};

export default AppRouter;
