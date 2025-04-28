import dynamic from "next/dynamic";
import { BiConfused } from "react-icons/bi";
import { IconType } from "react-icons/lib";
import { create } from "zustand";
import { CacheIcon, DynamicIconProps } from "../_types/icon";

type IconStoreProps = {
  getIcon: (props: DynamicIconProps) => any;
  loadedIcons: CacheIcon;
  iconLoading: (props: DynamicIconProps) => any;
  iconNotFound: any;
};

export const useIconStore = create<IconStoreProps>()((set, get) => ({
  loadedIcons: {},
  getIcon: (props: DynamicIconProps): any => {
    const store = get();
    const CacheIcon = store.loadedIcons[props.name];
    if (CacheIcon) {
      return <CacheIcon />;
    }
    const DynamicIcon = loadIcon(
      { name: props.name, lib: props.lib },
      store.iconLoading(props),
      store.iconNotFound
    );
    set((state) => ({
      loadedIcons: {
        ...state.loadedIcons,
        [props.name]: DynamicIcon,
      },
    }));
    return <DynamicIcon />;
  },
  iconLoading: (props: DynamicIconProps): any => {
    return () => (
      <div role="status" className={`animate-pulse relative`}>
        <div className="flex items-center">
          <div
            className={`rounded-full bg-gray-200 dark:text-gray-700 ${props.className}`}
          ></div>
        </div>
      </div>
    );
  },
  iconNotFound: () => {
    return (
      <BiConfused
        className="w-5 h-5 rounded-full"
        style={{ color: "#e91e63" }}
      />
    );
  },
}));

const loadIcon = (
  props: DynamicIconProps,
  iconLoading: any,
  iconNotFound: any
) => {
  const lib = getLibName(props);
  const loadedIcon = dynamic(
    () =>
      import(`react-icons/${lib}/index.js`).then((module) => {
        const LoadedIcon = module[props.name] as IconType;
        return LoadedIcon == undefined ? iconNotFound : LoadedIcon;
      }),
    {
      ssr: false,
      loading: iconLoading,
    }
  );

  return loadedIcon;
};

const getLibName = (props: DynamicIconProps) => {
  return !props.lib
    ? props.name
      .replace(/([a-z0-9])([A-Z])/g, "$1 $2")
      .split(" ")[0]
      .toLocaleLowerCase()
    : props.lib;
};
