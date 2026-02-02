import { useMutation, useQuery } from "@tanstack/react-query";
import { axiosInstance } from "../axios";

export function useRestApi<T>(url: string) {
  const { data: items, refetch: refetchItems } = useQuery({
    queryKey: [url],
    queryFn: () => axiosInstance.get<T[]>(url).then((res) => res.data),
  });

  const getByIdAsync = (id: number) => {
    // eslint-disable-next-line react-hooks/rules-of-hooks
    return useQuery({
      queryKey: [url, id],
      queryFn: () =>
        axiosInstance.get<T>(`${url}/${id}`).then((res) => res.data),
    });
  };
  const { mutateAsync: createItemAsync } = useMutation({
    mutationFn: (itemToCreate: T) =>
      axiosInstance.post(url, { ...itemToCreate }).then((res) => res.data),
  });
  const { mutateAsync: updateItemAsync } = useMutation({
    mutationFn: (itemToUpdate: T) =>
      axiosInstance.put(url, { ...itemToUpdate }).then((res) => res.data),
  });
  const { mutateAsync: deleteItemAsync } = useMutation({
    mutationFn: (id: number) =>
      axiosInstance.delete(`${url}/${id}`).then((res) => res.data),
  });

  return {
    items,
    refetchItems,
    getByIdAsync,
    createItemAsync,
    updateItemAsync,
    deleteItemAsync,
  };
}
