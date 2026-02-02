import { useAccount } from "../../hooks/useAccount";
import { Outlet } from "react-router";

export function AdminLayout() {
  const { isAdmin } = useAccount();

  return <>{isAdmin ? <Outlet /> : "Nincs megfelelő jogosultsága!"} </>;
}
