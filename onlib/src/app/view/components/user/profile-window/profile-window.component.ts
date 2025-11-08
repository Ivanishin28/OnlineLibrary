import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { MediaFileUploadComponent } from '../../_shared/media-file-upload/media-file-upload.component';
import { MediaFileId } from '../../../../business/models/_shared/mediaFileId';
import { UserAvatarComponent } from '../user-avatar/user-avatar.component';
import { ProfileService } from '../../../../business/services/user/profile.service';
import { AccountService } from '../../../../business/services/auth/account.service';
import { AuthService } from '../../../../business/services/auth/auth.service';
import { IdentityPreview } from '../../../../business/models/identity/identityPreview';
import { switchMap, take } from 'rxjs';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  standalone: true,
  selector: 'profile-window',
  imports: [
    CommonModule,
    MediaFileUploadComponent,
    UserAvatarComponent,
    InputTextModule,
  ],
  providers: [ProfileService],
  templateUrl: './profile-window.component.html',
  styleUrl: './profile-window.component.scss',
})
export class ProfileWindowComponent implements OnInit {
  public identity: IdentityPreview | undefined;
  public currentAvatarId: string | undefined;

  constructor(
    private ref: DynamicDialogRef,
    private profileService: ProfileService,
    private accountService: AccountService,
    private authService: AuthService
  ) {}

  public ngOnInit(): void {
    this.loadIdentity();
  }

  public onAvatarUploaded(fileId: MediaFileId): void {
    this.profileService.updateAvatar(fileId).subscribe(() => {
      this.currentAvatarId = fileId.value;
      if (this.identity) {
        this.identity = {
          ...this.identity,
          avatar_id: fileId.value,
        };
      }
    });
  }

  private loadIdentity(): void {
    this.authService.loggedUser$
      .pipe(
        take(1),
        switchMap((creds) => this.accountService.getIdentityBy(creds.userId))
      )
      .subscribe((identity) => {
        this.identity = identity;
        if (identity.avatar_id) {
          this.currentAvatarId = identity.avatar_id;
        }
      });
  }
}
