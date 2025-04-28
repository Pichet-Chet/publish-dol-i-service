type NavigationGroup = {
    resourceType: string;
    navigationData: Navigation[];
};

type Navigation = {
    id: string;
    name: string;
    url: string;
    order: string;
    icon: string;
    iconLib: string;
    showMenu: boolean;
    childrens: Navigation[];
};

type Route = {
    id: string;
    name: string;
    url: string;
};
