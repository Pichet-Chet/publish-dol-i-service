import { DrawerProps } from "antd";
import { create } from "zustand";

type DrawerStoreProps = DrawerProps & {
    // Custom
    //props: DrawerProps;
    //   toggleDrawer: () => void;
    //   openDrawer: () => void;
    setProps: (props: DrawerProps) => void;
    close: () => void;
};

export const useDrawerStore = create<DrawerStoreProps>()((set, get) => ({
    onClose: () => {
        get().close();
    },
    close: () => {
        set({ open: false });
    },
    //headerStyle: { border: "none" },
    contentWrapperStyle: {
        boxShadow: "none",
    },
    maskClosable: true,
    className: "shadow-xl",
    //bodyStyle: { paddingTop: "0" },
    mask: false,
    styles: { header: { border: "none" }, body: { paddingTop: "0" } },
    setProps: (props: DrawerProps) =>
        set((state) => {
            return { ...props };
        }),
    push: false,
}));
