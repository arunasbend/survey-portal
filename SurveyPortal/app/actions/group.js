import { ADD_GROUP, REMOVE_GROUP, RENAME_GROUP } from './constants';

let groupId = 3;

export const addGroup = (title) => {
  groupId += 1;
  return ({
    type: ADD_GROUP,
    id: groupId,
    title,
  });
};

export const removeGroup = id => ({
  type: REMOVE_GROUP,
  id,
});


export const renameGroup = (id, title) => ({
  type: RENAME_GROUP,
  id,
  title,
});
