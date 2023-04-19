import { useContext } from "react";
import { Routes, Route } from "react-router-dom";
import LoginPage from "../features/login-page/login";
import RegisterPage from "../features/register-page/register";
import { authContext } from "../cores/context/auth";
import AdminContainer from "../containers/admin.container";
import AppContainer from "../containers/app.container";
import LandingPage from "../features/landing-page/landingpage";
import HomePage from "../features/home-page/HomePage";
import NotFound from "../features/error/not-found";
import AdminManageJob from "../features/admin-manage-job";
import JobDetail from "../features/detail";

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
							<Route path="/" element={<AdminContainer />} />
							<Route path="/jobs" element={<AdminManageJob />} />
							<Route path="/detail/:id" element={<JobDetail />} />
						</>
					) : role === "BusinessEmployer" ? (
						<>
							<Route path="app" element={<AppContainer />} />
							<Route path="/" element={<HomePage />} />
							<Route path="/detail/:id" element={<JobDetail />} />
							{/* <Route path='/connections' element={<Connections />} />
              <Route path='detail/:id' element={<PostDetail />} />
              <Route path='user-profile/:id' element={<UserProfile />} /> */}
						</>
					) : (
						<>
							<Route path="app" element={<AppContainer />} />
							<Route path="/" element={<HomePage />} />
							<Route path="/detail/:id" element={<JobDetail />} />
						</>
					))}
				<Route path="*" element={<NotFound />} />
			</Route>
		</Routes>
	);
};

export default AppRouter;
