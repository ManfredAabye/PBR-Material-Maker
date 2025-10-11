export GIT_COMMITTER_NAME="Captain Ai"
export GIT_COMMITTER_EMAIL="extrude.ragu@proton.me"
git rebase -i --root --rebase-merges -x "git commit --amend --author=\"Captain Ai <extrude.ragu@proton.me>\" -C HEAD"