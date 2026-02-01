export GIT_COMMITTER_NAME="Manfred Aabye"
export GIT_COMMITTER_EMAIL="manfred.aabye@mail.com"
git rebase -i --root --rebase-merges -x "git commit --amend --author=\"Manfred Aabye <manfred.aabye@mail.com>\" -C HEAD"