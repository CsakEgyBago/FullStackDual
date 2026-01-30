import { Outlet } from "react-router";
import { Menu } from "../Menu/menu";
import { LoginForm } from "../LoginForm/LoginForm";
import { useAccessToken } from "../../hooks/useAccessToken";
import { LogoutButton } from "../LogoutButton/LogoutButton";

export function Layout() {
  const { accessToken, isLoading } = useAccessToken();

  return (
    <>
      {!isLoading ? (
        <>
          <div style={{ display: "flex", justifyContent: "space-between" }}>
            <div style={{ display: "flex", gap: 3 }}>
              <Menu />
            </div>
            <div style={{ display: "flex", gap: 3 }}>
              {!accessToken ? <LoginForm /> : <LogoutButton />}
            </div>
          </div>
          <Outlet />
        </>
      ) : (
        "betöltés..."
      )}
    </>
  );
}
