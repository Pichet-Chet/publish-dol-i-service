type Employee = {
    employeeId?: string;
    positionId?: string;
    positionAbbreviation?: string;
    positionName?: string;
    level?: string;
    orgCodeLv3?: string;
    orgIdLv3?: string;
    orgNameLv3?: string;
    orgCodeLv4?: string;
    orgIdLv4?: string;
    orgNameLv4?: string;
    orgCodeLv6?: string;
    orgIdLv6?: string;
    orgNameLv6?: string;
    companyAbbreviation?: string;
    companyId?: string;
    companyName?: string;
    statusId?: string;
    statusName?: string;
    initialName?: string;
    active?: string;
    fullNameTh?: string;
    fullNameEn?: string;
    userName?: string;
    email?: string;
    sub?: string;
    signature?: string;
};

type Role = {
    id: string;
    name: string;
    normalizedName: string;
    company: string;
    module: string;
    members: number;
    roleType: string;
    membersName: string;
    displayName: string;
    description: string;
    active: boolean;
};

type Permission = {
    name: string;
    roles: string;
    permissionRequirements: string;
    description: string;
    rolesName: string;
    clientId: number;
};

type PermissionApp = {
    name: string;
    view: boolean;
    create: boolean;
    edit: boolean;
    delete: boolean;
};

type PasswordInput = {
    currentPassword: string;
    newPassword: string;
    confirmPassword: string;
};